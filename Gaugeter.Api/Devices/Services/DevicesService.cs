using System.Threading.Tasks;
using Gaugeter.Api.Devices.Models;
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
            var existingDevice = await _devicesRepository.GetDevice(device.BluetoothAddress);

            if (existingDevice == null)
            {
                await _devicesRepository.CreateDevice(device);
                existingDevice = await _devicesRepository.GetDevice(device.BluetoothAddress);
            }

            return await _devicesRepository.AddDeviceToUser(userId, existingDevice);
        }
    }
}
