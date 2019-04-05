using System.Threading.Tasks;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Services.Devices
{
    public interface IDevicesService
    {
        Task<EntityState> AddDeviceToUser(string userId, Device device);
    }
}
