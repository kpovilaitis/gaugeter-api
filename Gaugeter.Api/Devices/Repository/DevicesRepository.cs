using System;
using System.Threading.Tasks;
using Gaugeter.Api.Data;
using Gaugeter.Api.Devices.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Devices.Repository
{
    public class DevicesRepository : IDevicesRepository
    {
        private readonly GaugeterDbContext _context;

        public DevicesRepository(GaugeterDbContext context)
        {
            _context = context;
        }

        public async Task<EntityState> AddDeviceToUser(string userId, Device device)
        {
            throw new NotImplementedException();
        }

        public async Task<EntityState> CreateDevice(Device device)
        {
            var state = await _context.Device.AddAsync(device);

            await _context.SaveChangesAsync();

            return state.State;
        }

        public async Task<Device> GetDevice(string bluetoothAddress)
        {
            return await _context.Device.SingleOrDefaultAsync(d => d.BluetoothAddress == bluetoothAddress);
        }
    }
}
