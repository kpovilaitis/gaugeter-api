using System.Threading.Tasks;
using CarGaugesApi.Models;
using CarGaugesApi.Repository.DevicesRepo;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Services.Devices
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
