using Microsoft.Extensions.DependencyInjection;

namespace Millsnet.DvlApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
            {
                fonts.AddFont("icofont.ttf", "icofont");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			})
            .RegisterServices()
            .RegisterViewModels()
            .RegisterViews();

        //RegisterRoutes();
		return builder.Build();
	}

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<ViewModels.HomeViewModel>();
        builder.Services.AddSingleton<ViewModels.SettingsViewModel>();
        builder.Services.AddSingleton<ViewModels.EditVehicleViewModel>();

        return builder;
    }
    public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
    {
        builder.Services.AddTransient<Views.HomeView>();
        builder.Services.AddTransient<Views.SettingsView>();
        builder.Services.AddTransient<Views.EditVehicleView>();

        return builder;
    }
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {
        builder.Services.AddSingleton<Interfaces.IAlertService, Services.AlertService>();
        builder.Services.AddSingleton<Interfaces.IDataService, Services.DataService>();
        builder.Services.AddSingleton<Interfaces.IDvlaService, Services.DvlaService>();
        builder.Services.AddSingleton<Interfaces.INavigationService, Services.NavigationService>();

        return builder;
    }

    public static void RegisterRoutes()
    {
        Routing.RegisterRoute("Home", typeof(Views.HomeView));
        Routing.RegisterRoute("Settings", typeof(Views.SettingsView));
    }
}
