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
        private string _RegistrationNumber;
        public string RegistrationNumber { get => _RegistrationNumber; set => SetProperty(ref _RegistrationNumber, value); }

        private string _Model;

        public string Model { get => _Model; set => SetProperty(ref _Model, value); }

        public EditVehicleViewModel() { }
        public EditVehicleViewModel(IDataService dataService, IAlertService alertService)
        {
            _DataService = dataService;
            _AlertService = alertService;
        }

        public void LoadVehicle(VehicleDetails vehicle) 
        {
            RegistrationNumber = vehicle.RegistrationNumber;
            Model = vehicle.Model;
        }

        public ICommand SaveCommand => new Command(() =>
        {
            List<VehicleDetails> vehicles = _DataService.Load<IEnumerable<VehicleDetails>>(nameof(VehicleDetails))?.ToList()??new List<VehicleDetails>();
            VehicleDetails vehicle = vehicles.FirstOrDefault(v => v.RegistrationNumber == _RegistrationNumber)??new VehicleDetails 
            {
                RegistrationNumber = _RegistrationNumber,
                Model = _Model
            };
            vehicles = vehicles.Where(v => v.RegistrationNumber != _RegistrationNumber)?.ToList();
            vehicle.RegistrationNumber = _RegistrationNumber;
            vehicle.Model = _Model;
            vehicles.Add(vehicle);
            _DataService.Save(vehicles, nameof(VehicleDetails));

            _AlertService.ShowAlert("Settings", "Vehicle saved");
        });
    }
}
