using Millsnet.DvlApp.Interfaces;
using Millsnet.DvlApp.Models;
using System.Net.Http.Json;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace Millsnet.DvlApp.Services
{
    public class DvlaService : IDvlaService
    {
        private readonly string _ApiKey;

        public DvlaService()
        {
            _ApiKey = "EV8AC0aJkp735WZO4rUVD3S8p6AKk0AR6HGx2o6r";
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