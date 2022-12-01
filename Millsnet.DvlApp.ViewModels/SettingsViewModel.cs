using Millsnet.DvlApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Millsnet.DvlApp.ViewModels
{
    public class SettingsViewModel:ViewModelBase
    {
        private string _ApiKey;
        public string ApiKey { get => _ApiKey; set => SetProperty(ref _ApiKey, value); }

        private readonly IAlertService _AlertService;

        public SettingsViewModel()
        {
            Initialise();
        }

        public SettingsViewModel(IAlertService alertService)
        {
            _AlertService = alertService;
            Initialise();
        }

        private void Initialise()
        {
            ApiKey = Preferences.Get(nameof(ApiKey), string.Empty);
        }

        public ICommand SaveCommand => new Command(() =>
        {
            Preferences.Set(nameof(ApiKey), _ApiKey);
            _AlertService.ShowAlert("Settings", "Settings saved");
        });
    }
}
