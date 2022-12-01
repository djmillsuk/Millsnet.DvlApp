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
        private IEnumerable<VehicleDetails> _Vehicles;
        public IEnumerable<VehicleDetails> Vehicles { get => _Vehicles; set => SetProperty(ref _Vehicles, value); }

        public HomeViewModel() { }
        public HomeViewModel(EditVehicleViewModel editVehicleViewModel, IDataService dataService, IDvlaService dvlaService)
        {
            _EditVehicleViewModel = editVehicleViewModel;
            _DataService = dataService;
            _DvlaService = dvlaService;
            Vehicles = _DataService.Load<IEnumerable<VehicleDetails>>(nameof(VehicleDetails));
        }


        public ICommand AddVehicleCommand => new Command(async () =>
        {
            _EditVehicleViewModel.LoadVehicle(new VehicleDetails { RegistrationNumber = "N3WR3G", Model = "New Car" });
            await Shell.Current.GoToAsync("EditVehicle");
        });
        public ICommand RefreshVehicleCommand => new Command<VehicleDetails>(async (vehicle) =>
        {
            VehicleDetails details = await _DvlaService.GetVehicleAsync(vehicle.RegistrationNumber);
            List<VehicleDetails> newDetails = (_Vehicles?.ToList()??new List<VehicleDetails>()).Where(v => v.RegistrationNumber != vehicle.RegistrationNumber).ToList();
            newDetails.Add(details);
            Vehicles = newDetails;
            _DataService.Save(Vehicles, nameof(VehicleDetails));
        });
    }
}
