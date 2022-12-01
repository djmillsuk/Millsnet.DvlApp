using Millsnet.DvlApp.Interfaces;
using Millsnet.DvlApp.Models;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Millsnet.DvlApp.Services
{
    public class DvlaService : IDvlaService
    {
        private readonly IDataService _DataService;
        private readonly string _ApiKey;

        public DvlaService(IDataService dataService)
        {
            _DataService = dataService;
            _ApiKey = _DataService.Load("ApiKey");
        }
        public async Task<VehicleDetails> GetVehicleAsync(string registrationNumber)
        {
            using(HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://driver-vehicle-licensing.api.gov.uk");
                client.DefaultRequestHeaders.Add("x-api-key",_ApiKey);
                HttpResponseMessage response = await client.PostAsJsonAsync("vehicle-enquiry/v1/vehicles", new { registrationNumber });
                string json = await response.Content.ReadAsStringAsync();
                VehicleDetails ret = JsonSerializer.Deserialize<VehicleDetails>(json);
                ret.LastChecked = DateTime.Now;
                return ret;
            }
        }
    }
}