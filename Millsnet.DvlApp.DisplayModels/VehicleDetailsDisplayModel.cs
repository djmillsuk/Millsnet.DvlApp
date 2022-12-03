using Millsnet.DvlApp.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Millsnet.DvlApp.DisplayModels
{
    public class VehicleDetailsDisplayModel:DisplayModelBase
    {
        private string _ArtEndDate;
        public string ArtEndDate { get => _ArtEndDate; set => SetProperty(ref _ArtEndDate,value); }
        private int _Co2Emissions;
        public int Co2Emissions { get => _Co2Emissions; set => SetProperty(ref _Co2Emissions,value); }
        private string _Colour;
        public string Colour { get => _Colour; set=> SetProperty(ref _Colour,value); }
        private int _EngineCapacity;
        public int EngineCapacity { get => _EngineCapacity; set => SetProperty(ref _EngineCapacity, value); }
        private string _FuelType;
        public string FuelType { get => _FuelType; set=> SetProperty(ref _FuelType,value); }
        private string _Make;
        public string Make { get => _Make; set => SetProperty(ref _Make, value); }
        private string _Model;
        public string Model { get => _Model; set => SetProperty(ref _Model, value);  }
        private bool _MarkedForExport;
        public bool MarkedForExport { get => _MarkedForExport; set => SetProperty(ref _MarkedForExport, value); }
        private string _MonthOfFirstRegistration;
        public string MonthOfFirstRegistration { get => _MonthOfFirstRegistration; set => SetProperty(ref _MonthOfFirstRegistration, value);  }
        private string _MotStatus;
        public string MotStatus { get => _MotStatus; set => SetProperty(ref _MotStatus, value);  }
        private DateTime _MotExpiryDate;
        public DateTime MotExpiryDate { get => _MotExpiryDate; set => SetProperty(ref _MotExpiryDate, value); }
        private string _RegistrationNumber;
        public string RegistrationNumber { get => _RegistrationNumber; set => SetProperty(ref _RegistrationNumber, value); }
        private int _RevenueWeight;
        public int RevenueWeight { get => _RevenueWeight; set => SetProperty(ref _RevenueWeight, value); }
        private string _TaxDueDate;
        public string TaxDueDate { get => _TaxDueDate; set => SetProperty(ref _TaxDueDate, value);  }
        private string _TaxStatus;
        public string TaxStatus { get => _TaxStatus; set => SetProperty(ref _TaxStatus, value); }
        private string _TypeApproval;
        public string TypeApproval { get => _TypeApproval; set => SetProperty(ref _TypeApproval, value); }
        private string _WheelPlan;
        public string Wheelplan { get => _WheelPlan; set => SetProperty(ref _WheelPlan, value); }
        private int _YearOfManufacture;
        public int YearOfManufacture { get => _YearOfManufacture; set => SetProperty(ref _YearOfManufacture, value); }
        private string _EuroStatus;
        public string EuroStatus { get => _EuroStatus; set => SetProperty(ref _EuroStatus, value); }
        private string _RealDrivingEmissions;
        public string RealDrivingEmissions { get => _RealDrivingEmissions; set => SetProperty(ref _RealDrivingEmissions, value); }
        private string _DateOfLastV5CIssued;
        public string DateOfLastV5CIssued { get => _DateOfLastV5CIssued; set => SetProperty(ref _DateOfLastV5CIssued, value); }
        private DateTime _LastChecked;
        public DateTime LastChecked { get => _LastChecked; set => SetProperty(ref _LastChecked, value); }
        public VehicleDetailsDisplayModel() { }
        public VehicleDetailsDisplayModel(VehicleDetails m)
        {
            ArtEndDate = m.ArtEndDate;
            Co2Emissions = m.Co2Emissions;
            Colour = m.Colour;
            EngineCapacity = m.EngineCapacity;
            FuelType = m.FuelType;
            Make = m.Make;
            Model = m.Model;
            MarkedForExport = m.MarkedForExport;
            MonthOfFirstRegistration = m.MonthOfFirstRegistration;
            MotStatus = m.MotStatus;
            MotExpiryDate = m.MotExpiryDate;
            RegistrationNumber = m.RegistrationNumber;
            RevenueWeight = m.RevenueWeight;
            TaxDueDate = m.TaxDueDate;
            TaxStatus = m.TaxStatus;
            TypeApproval = m.TypeApproval;
            Wheelplan = m.Wheelplan;
            YearOfManufacture = m.YearOfManufacture;
            EuroStatus = m.EuroStatus;
            RealDrivingEmissions = m.RealDrivingEmissions;
            DateOfLastV5CIssued = m.DateOfLastV5CIssued;
            LastChecked = m.LastChecked;
        }

        public void UpdateDetails(VehicleDetails m)
        {
            ArtEndDate = m.ArtEndDate;
            Co2Emissions = m.Co2Emissions;
            Colour = m.Colour;
            EngineCapacity = m.EngineCapacity;
            FuelType = m.FuelType;
            Make = m.Make;
            MarkedForExport = m.MarkedForExport;
            MonthOfFirstRegistration = m.MonthOfFirstRegistration;
            MotStatus = m.MotStatus;
            MotExpiryDate = m.MotExpiryDate;
            RevenueWeight = m.RevenueWeight;
            TaxDueDate = m.TaxDueDate;
            TaxStatus = m.TaxStatus;
            TypeApproval = m.TypeApproval;
            Wheelplan = m.Wheelplan;
            YearOfManufacture = m.YearOfManufacture;
            EuroStatus = m.EuroStatus;
            RealDrivingEmissions = m.RealDrivingEmissions;
            DateOfLastV5CIssued = m.DateOfLastV5CIssued;
            LastChecked = m.LastChecked;
        }
        public VehicleDetails ToVehicleDetails()
        {
            VehicleDetails m = new VehicleDetails();
            m.ArtEndDate = ArtEndDate;
            m.Co2Emissions = Co2Emissions;
            m.Colour = Colour;
            m.EngineCapacity = EngineCapacity;
            m.FuelType = FuelType;
            m.Make = Make;
            m.Model = Model;
            m.MarkedForExport = MarkedForExport;
            m.MonthOfFirstRegistration = MonthOfFirstRegistration;
            m.MotStatus = MotStatus;
            m.MotExpiryDate = MotExpiryDate;
            m.RegistrationNumber = RegistrationNumber;
            m.RevenueWeight = RevenueWeight;
            m.TaxDueDate = TaxDueDate;
            m.TaxStatus = TaxStatus;
            m.TypeApproval = TypeApproval;
            m.Wheelplan = Wheelplan;
            m.YearOfManufacture = YearOfManufacture;
            m.EuroStatus = EuroStatus;
            m.RealDrivingEmissions = RealDrivingEmissions;
            m.DateOfLastV5CIssued = DateOfLastV5CIssued;
            m.LastChecked = LastChecked;

            return m;
        }
    }
}
