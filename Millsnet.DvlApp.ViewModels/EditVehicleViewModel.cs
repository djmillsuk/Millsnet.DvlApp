using Millsnet.DvlApp.DisplayModels;
using Millsnet.DvlApp.Interfaces;
using Millsnet.DvlApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Millsnet.DvlApp.ViewModels
{
    public class EditVehicleViewModel:ViewModelBase
    {
        private readonly IDataService _DataService;
        private readonly IAlertService _AlertService;
        private VehicleDetailsDisplayModel _Vehicle;
        public VehicleDetailsDisplayModel Vehicle { get => _Vehicle; set => SetProperty(ref _Vehicle, value); }

        public EditVehicleViewModel() { }
        public EditVehicleViewModel(IDataService dataService, IAlertService alertService)
        {
            _DataService = dataService;
            _AlertService = alertService;
        }

        public void LoadVehicle(VehicleDetailsDisplayModel vehicle) 
        {
            Vehicle = vehicle;
        }

        public ICommand SaveCommand => new Command(() =>
        {
            Task.Run(async () =>
            {
                IsBusy = true;
                await Task.Delay(1000);
                IsBusy = false;
                await Shell.Current.Navigation.PopModalAsync();
            });
            //List<VehicleDetails> vehicles = _DataService.Load<IEnumerable<VehicleDetails>>(nameof(VehicleDetails))?.ToList()??new List<VehicleDetails>();
            //VehicleDetails vehicle = vehicles.FirstOrDefault(v => v.RegistrationNumber == _RegistrationNumber)??new VehicleDetails 
            //{
            //    RegistrationNumber = _RegistrationNumber,
            //    Model = _Model
            //};
            //vehicles = vehicles.Where(v => v.RegistrationNumber != _RegistrationNumber)?.ToList();
            //vehicle.RegistrationNumber = _RegistrationNumber;
            //vehicle.Model = _Model;
            //vehicles.Add(vehicle);
            //_DataService.Save(vehicles, nameof(VehicleDetails));

            //_AlertService.ShowAlert("Settings", "Vehicle saved");
        });
    }
}
