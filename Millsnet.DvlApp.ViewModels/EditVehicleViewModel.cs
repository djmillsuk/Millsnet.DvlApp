﻿using Microsoft.Maui.Platform;
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
        private readonly INavigationService _NavigationService;
        private VehicleDetailsDisplayModel _Vehicle;
        string _OriginalPlate;
        public VehicleDetailsDisplayModel Vehicle { get => _Vehicle; set => SetProperty(ref _Vehicle, value); }

        public EditVehicleViewModel() { }
        public EditVehicleViewModel(IDataService dataService, IAlertService alertService, INavigationService navigationService)
        {
            _DataService = dataService;
            _AlertService = alertService;
            _NavigationService = navigationService;
        }

        public override Task InitialiseAsync(object data)
        {
            _OriginalPlate = string.Empty;
            if (data != null)
            {
                if (data is VehicleDetailsDisplayModel)
                {
                    Vehicle = (VehicleDetailsDisplayModel)data;
                    _OriginalPlate = Vehicle.RegistrationNumber;
                }
            }
            return base.InitialiseAsync(data);
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

                IEnumerable<VehicleDetails> vehicles = _DataService.Load<IEnumerable<VehicleDetails>>(nameof(VehicleDetails)) ?? new List<VehicleDetails>();

                var newVehicles = vehicles.Where(v => v.RegistrationNumber != _OriginalPlate).ToList();
                newVehicles.Add(_Vehicle.ToVehicleDetails());

                _DataService.Save(newVehicles, nameof(VehicleDetails));

                IsBusy = false;
                await _NavigationService.BackAsync(newVehicles);
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

        public ICommand DeleteCommand => new Command(() =>
        {
            Task.Run(() =>
            {
                _AlertService.ShowConfirmation("Confirmation", "Delete this vehicle?", async (IsConfirmed) =>
                {
                    if (IsConfirmed)
                    {
                        IsBusy = true;

                        IEnumerable<VehicleDetails> vehicles = _DataService.Load<IEnumerable<VehicleDetails>>(nameof(VehicleDetails)) ?? new List<VehicleDetails>();

                        var newVehicles = vehicles.Where(v => v.RegistrationNumber != _Vehicle.RegistrationNumber).ToList();

                        _DataService.Save(newVehicles, nameof(VehicleDetails));

                        IsBusy = false;
                        await _NavigationService.BackAsync(newVehicles);
                    }
                });

            });
        });
    }
}
