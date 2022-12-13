using Millsnet.DvlApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Millsnet.DvlApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _ServiceProvider;
        private List<string> _RegisteredRoutes = new List<string>();

        public NavigationService(IServiceProvider serviceProvider)
        {
            _ServiceProvider = serviceProvider;
        }

        public void Initialise()
        {
            Shell.Current.Navigated += ShellNavigated;
        }

        private void ShellNavigated(object sender, ShellNavigatedEventArgs e)
        {
            Page page = Shell.Current.CurrentPage;
            page.BindingContext = GetViewModel(page.GetType());
            OnInitialised(null);
        }

        public async Task NavigateAsync<T>(object data = null) where T : class
        {
            Page view = GetPage<T>();
            Type viewType = view.GetType();
            string route = viewType.Name.ToLower();
            if (!_RegisteredRoutes.Any(r => r == route))
            {
                Routing.RegisterRoute(route, viewType);
                _RegisteredRoutes.Add(route);
            }
            await Shell.Current.GoToAsync(route);
            if (data != null)
            {
                object viewModel = GetViewModel(viewType);
                if (viewModel != null)
                {
                    MethodInfo initialise = viewModel.GetType().GetMethod("InitialiseAsync");
                    initialise.Invoke(viewModel, new object[] { data });
                }
            }
        }

        public async Task BackAsync(object data = null)
        {
            if (Shell.Current.Navigation.NavigationStack.Any())
            {
                await MainThread.InvokeOnMainThreadAsync(async () => await Shell.Current.GoToAsync(".."));
                if (data != null)
                {
                    object viewModel = GetViewModel(Shell.Current.CurrentPage.GetType());
                    if (viewModel != null)
                    {
                        MethodInfo initialise = viewModel.GetType().GetMethod("InitialiseAsync");
                        initialise.Invoke(viewModel, new object[] { data });
                    }
                }
            }
        }

        private Page GetPage<T>() where T : class
        {
            if (!typeof(T).Name.EndsWith("ViewModel"))
            {
                throw new Exception($"Invalid type for ViewModel ({typeof(T).Name})");
            }

            string ns = typeof(T).Namespace;
            string typeFullName = $"{ns}.{typeof(T).Name}".Replace("ViewModel", "View");
            Type type = GetType(typeFullName);
            return (Page)_ServiceProvider.GetRequiredService(type);
        }
        private object GetViewModel(Type pageType)
        {
            if (!pageType.Name.EndsWith("View"))
            {
                throw new Exception($"Invalid type for View ({pageType.Name})");
            }

            string ns = pageType.Namespace;
            string typeFullName = $"{ns}.{pageType.Name}".Replace("View", "ViewModel");
            Type type = GetType(typeFullName);
            return _ServiceProvider.GetRequiredService(type);
        }
        private static Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = a.GetType(typeName);
                if (type != null)
                    return type;
            }
            return null;
        }
        protected virtual void OnInitialised(EventArgs args)
        {
            EventHandler<EventArgs> handler = Initialised;
            if (handler != null)
            {
                handler(this, args);
            }
        }


        public event EventHandler<EventArgs> Initialised;
    }
}
