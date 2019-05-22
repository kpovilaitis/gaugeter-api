using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Devices.Models.Data;
using Gaugeter.Api.Devices.Repository;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Services.Devices
{
    public class DevicesService : IDevicesService
    {
        private readonly IDevicesRepository _devicesRepository;

        public DevicesService(IDevicesRepository devicesRepository)
        {
            _devicesRepository = devicesRepository;
        }

        public async Task<EntityState> AddDeviceToUser(string userId, Device device)
        {
            return await _devicesRepository.AddDeviceToUser(userId, device);
        }

        public async Task<EntityState> Create(Device device)
        {
            return await _devicesRepository.Create(device);
        }

        public async Task<Device> Get(string bluetoothAddress)
        {
            return await _devicesRepository.Get(bluetoothAddress);
        }

        public async Task<EntityState> Update(Device device)
        {
            return await _devicesRepository.Update(device);
        }

        public async Task<IEnumerable<Device>> GetUserDevices(string userId)
        {
            return await _devicesRepository.GetUserDevices(userId);
        }

        public async Task<EntityState> Remove(string userId, string bluetoothAddress)
        {
            return await _devicesRepository.Remove(userId, bluetoothAddress);
        }
    }
}
