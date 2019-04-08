using System.Threading.Tasks;
using Gaugeter.Api.Devices.Models;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Devices.Repository
{
    public interface IDevicesRepository
    {
        Task<Device> GetDevice(string bluetoothAddress);

        Task<EntityState> CreateDevice(Device device);

        Task<EntityState> AddDeviceToUser(string userId, Device device);
    }
}
