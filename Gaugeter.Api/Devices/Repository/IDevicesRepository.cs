using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Devices.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Devices.Repository
{
    public interface IDevicesRepository
    {
        Task<EntityState> Create(Device device);

        Task<EntityState> AddDeviceToUser(string userId, Device device);

        Task<Device> Get(string bluetoothAddress);

        Task<IEnumerable<Device>> GetUserDevices(string userId);

        Task<EntityState> Remove(string userId, string bluetoothAddress);
    }
}
