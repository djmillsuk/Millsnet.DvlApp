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

        private bool _VehiclesSelected;
        public bool VehiclesSelected { get => _VehiclesSelected; set => SetProperty(ref _VehiclesSelected, value); }
        private ObservableCollection<VehicleDetailsDisplayModel> _Vehicles = new ObservableCollection<VehicleDetailsDisplayModel>();
        public ObservableCollection<VehicleDetailsDisplayModel> Vehicles { get => _Vehicles; set => SetProperty(ref _Vehicles, value); }

        public HomeViewModel() { }
        public HomeViewModel(EditVehicleViewModel editVehicleViewModel, IDataService dataService, IDvlaService dvlaService, IAlertService alertService)
        {
            _AlertService = alertService;
            _EditVehicleViewModel = editVehicleViewModel;
            _DataService = dataService;
            _DvlaService = dvlaService;
            Vehicles.CollectionChanged += VehiclesChanged;
            IEnumerable<VehicleDetails> vehicles = _DataService.Load<IEnumerable<VehicleDetails>>(nameof(VehicleDetails))??new List<VehicleDetails>();
            foreach (VehicleDetails vehicle in vehicles) 
            Vehicles.Add(new VehicleDetailsDisplayModel(vehicle));
        }

        private void VehiclesChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            IEnumerable<VehicleDetailsDisplayModel> vehicles = e?.NewItems?.Cast<VehicleDetailsDisplayModel>()?? new List<VehicleDetailsDisplayModel>();
            foreach (VehicleDetailsDisplayModel vehicle in vehicles)
            {
                vehicle.PropertyChanged += (sender,e) => 
                {
                    VehiclesSelected = _Vehicles.Any(v => v.IsSelected == true);
                };
            }
            VehiclesSelected = _Vehicles.Any(v => v.IsSelected == true);
        }

        public ICommand AddVehicleCommand => new Command(async () =>
        {
            VehicleDetailsDisplayModel vehicle = new VehicleDetailsDisplayModel { RegistrationNumber = "N3WR3G", Model = "New Car" };
            Vehicles.Add(vehicle);
            _DataService.Save(Vehicles.Select(v => v.ToVehicleDetails()), nameof(VehicleDetails));
        });
        public ICommand EditVehicleCommand => new Command(async () =>
        {
            VehicleDetailsDisplayModel vehicle = Vehicles.FirstOrDefault(v => v.IsSelected);
            _EditVehicleViewModel.LoadVehicle(vehicle);
            await Shell.Current.GoToAsync("EditVehicle");
        });

        public ICommand RefreshVehiclesCommand => new Command(async () =>
        {
            foreach (var vehicle in _Vehicles?.Where(v => v.IsSelected))
            {
                VehicleDetails details = await _DvlaService.GetVehicleAsync(vehicle.RegistrationNumber);
                if (details.RegistrationNumber == null)
                {
                    _AlertService.ShowAlert("Error","Vehicle registration is not valid or vehicle details are not found");
                    return;
                }
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
