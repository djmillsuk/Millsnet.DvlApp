using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Millsnet.DvlApp.DisplayModels
{
    // All the code in this file is included in all platforms.
    public class DisplayModelBase : INotifyPropertyChanged
    {
        private bool _IsSelected;
        public bool IsSelected { get => _IsSelected; set => SetProperty(ref _IsSelected, value); }

        public event PropertyChangedEventHandler PropertyChanged;

        //[NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value)) return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        public ICommand SelectCommand => new Command(() =>
        {
            IsSelected = true;
        });
        public ICommand DeSelectCommand => new Command(() =>
        {
            IsSelected = false;
        });
        public ICommand ToggleSelectCommand => new Command(() =>
        {
            IsSelected = !_IsSelected;
        });
    }
}