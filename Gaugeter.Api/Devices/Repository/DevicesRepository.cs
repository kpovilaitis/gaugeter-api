using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gaugeter.Api.Data;
using Gaugeter.Api.Devices.Models.Data;
using Gaugeter.Api.Users.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Devices.Repository
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
            if (null != await _context.UserDevice.FindAsync(userId, device.BluetoothAddress))
                return EntityState.Unchanged;
            
            var userEntity = await _context.User.FindAsync(userId);

            await _context.UserDevice.AddAsync(new UserDevice
            {
                User = userEntity,
                UserId = userId,
                BluetoothAddress = device.BluetoothAddress,
                Device = await _context.Device.FindAsync(device.BluetoothAddress) ?? device
            });

            await _context.SaveChangesAsync();

            return EntityState.Added;
        }

        public async Task<EntityState> Create(Device device)
        {
            var deviceEntityState = _context.Entry(device).State = EntityState.Added;

            await _context.SaveChangesAsync();

            return deviceEntityState;
        }

        public async Task<Device> Get(string bluetoothAddress)
        {
            return await _context.Device.FindAsync(bluetoothAddress);
        }

        public async Task<IEnumerable<Device>> GetUserDevices(string userId)
        {
            return await _context.UserDevice
                    .Include(d => d.Device)
                    .Where(arg => arg.UserId == userId)
                    .Select(arg => arg.Device)
                    .ToListAsync();
        }

        public async Task<EntityState> Remove(string userId, string bluetoothAddress)
        {
            var userDeviceEntity = await _context.UserDevice.FindAsync(userId, bluetoothAddress);

            if (userDeviceEntity == null)
                return EntityState.Unchanged;

            var userDeviceEntityState = _context.Entry(userDeviceEntity).State = EntityState.Deleted;
            
            await _context.SaveChangesAsync();

            return userDeviceEntityState;
        }
    }
}
