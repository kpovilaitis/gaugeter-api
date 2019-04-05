using System;
using System.Threading.Tasks;
using CarGaugesApi.Data;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Repository.DevicesRepo
{
    public class DevicesRepository : IDevicesRepository
    {
        private readonly CarGaugesDbContext _context;

        public DevicesRepository(CarGaugesDbContext context)
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
