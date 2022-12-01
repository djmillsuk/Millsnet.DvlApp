using Millsnet.DvlApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Millsnet.DvlApp.Interfaces
{
    public interface IDvlaService
    {
        Task<VehicleDetails> GetVehicleAsync(string registrationNumber);
    }
}
