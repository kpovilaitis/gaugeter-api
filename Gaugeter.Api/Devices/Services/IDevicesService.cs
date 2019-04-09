using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Devices.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Services.Devices
{
    public interface IDevicesService
    {
        Task<EntityState> AddDeviceToUser(string userId, Device device);

        Task<EntityState> Create(Device device);

        Task<Device> Get(string bluetoothAddress);

        Task<IEnumerable<Device>> GetUserDevices(string userId);

        Task<EntityState> Remove(string userId, string bluetoothAddress);
    }
}
