using Millsnet.DvlApp.DisplayModels;
using Millsnet.DvlApp.Interfaces;
using Millsnet.DvlApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Millsnet.DvlApp.ViewModels
{
    public class HomeViewModel:ViewModelBase
    {
        private readonly EditVehicleViewModel _EditVehicleViewModel;
        private readonly IDataService _DataService;
        private readonly IDvlaService _DvlaService;
        private readonly IAlertService _AlertService;
        private ObservableCollection<VehicleDetailsDisplayModel> _Vehicles;
        public ObservableCollection<VehicleDetailsDisplayModel> Vehicles { get => _Vehicles; set => SetProperty(ref _Vehicles, value); }

        public HomeViewModel() { }
        public HomeViewModel(EditVehicleViewModel editVehicleViewModel, IDataService dataService, IDvlaService dvlaService, IAlertService alertService)
        {
            _AlertService = alertService;
            _EditVehicleViewModel = editVehicleViewModel;
            _DataService = dataService;
            _DvlaService = dvlaService;
            IEnumerable<VehicleDetails> vehicles = _DataService.Load<IEnumerable<VehicleDetails>>(nameof(VehicleDetails));
            Vehicles = new ObservableCollection<VehicleDetailsDisplayModel>(vehicles.Select(v => new VehicleDetailsDisplayModel(v)));
        }


        public ICommand AddVehicleCommand => new Command(async () =>
        {
            _EditVehicleViewModel.LoadVehicle(new VehicleDetails { RegistrationNumber = "N3WR3G", Model = "New Car" });
            await Shell.Current.GoToAsync("EditVehicle");
        });
        public ICommand RefreshVehiclesCommand => new Command(async () =>
        {
            foreach (var vehicle in _Vehicles?.Where(v => v.IsSelected))
            {
                VehicleDetails details = await _DvlaService.GetVehicleAsync(vehicle.RegistrationNumber);
                VehicleDetailsDisplayModel vehicleToUpdate = _Vehicles.First(v => v.RegistrationNumber == details.RegistrationNumber);
                vehicleToUpdate.UpdateDetails(details);
                _DataService.Save(Vehicles.Select(v => v.ToVehicleDetails()), nameof(VehicleDetails));
                vehicleToUpdate.IsSelected = false;
            }
        });
        public ICommand DeleteVehiclesCommand => new Command(() =>
        {
            _AlertService.ShowConfirmation("Confirmation", "Delete selected vehicles?", IsConfirmed =>
            {
                if (IsConfirmed)
                {
                    foreach (var v in _Vehicles.Where(v => v.IsSelected == true).ToList())
                    {
                        _Vehicles.Remove(v);
                    }
                    _DataService.Save(Vehicles.Select(v => v.ToVehicleDetails()), nameof(VehicleDetails));
                }
            });
        });
    }
}
