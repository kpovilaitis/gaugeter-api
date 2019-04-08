using System.Threading.Tasks;
using Gaugeter.Api.Devices.Models;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Services.Devices
{
    public interface IDevicesService
    {
        Task<EntityState> AddDeviceToUser(string userId, Device device);
    }
}
