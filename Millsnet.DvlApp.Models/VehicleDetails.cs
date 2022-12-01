using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Millsnet.DvlApp.Models
{
    public class VehicleDetails
    {
        [JsonPropertyName("artEndDate")]
        public string ArtEndDate { get; set; }

        [JsonPropertyName("co2Emissions")]
        public int Co2Emissions { get; set; }

        [JsonPropertyName("colour")]
        public string Colour { get; set; }

        [JsonPropertyName("engineCapacity")]
        public int EngineCapacity { get; set; }

        [JsonPropertyName("fuelType")]
        public string FuelType { get; set; }

        [JsonPropertyName("make")]
        public string Make { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("markedForExport")]
        public bool MarkedForExport { get; set; }

        [JsonPropertyName("monthOfFirstRegistration")]
        public string MonthOfFirstRegistration { get; set; }

        [JsonPropertyName("motStatus")]
        public string MotStatus { get; set; }

        [JsonPropertyName("motExpiryDate")]
        public DateTime MotExpiryDate { get; set; }

        [JsonPropertyName("registrationNumber")]
        public string RegistrationNumber { get; set; }

        [JsonPropertyName("revenueWeight")]
        public int RevenueWeight { get; set; }

        [JsonPropertyName("taxDueDate")]
        public string TaxDueDate { get; set; }

        [JsonPropertyName("taxStatus")]
        public string TaxStatus { get; set; }

        [JsonPropertyName("typeApproval")]
        public string TypeApproval { get; set; }

        [JsonPropertyName("wheelplan")]
        public string Wheelplan { get; set; }

        [JsonPropertyName("yearOfManufacture")]
        public int YearOfManufacture { get; set; }

        [JsonPropertyName("euroStatus")]
        public string EuroStatus { get; set; }

        [JsonPropertyName("realDrivingEmissions")]
        public string RealDrivingEmissions { get; set; }

        [JsonPropertyName("dateOfLastV5CIssued")]
        public string DateOfLastV5CIssued { get; set; }

        [JsonPropertyName("lastChecked")]
        public DateTime LastChecked { get; set; }
    }
}
