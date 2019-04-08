using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gaugeter.Api.Data;
using Gaugeter.Api.Devices.Models.Data;
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
            var user = await _context.User.FindAsync(userId);

            if (user.Devices == null)
                user.Devices = new List<Device>();

            user.Devices.Add(device);

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
            try
            {
                var userEntity = await _context.User.Include(u => u.Devices).SingleAsync(i => i.UserId == userId);

                return userEntity.Devices.ToList();
            }
            catch
            {
                return null;
            }
        }

        public async Task<EntityState> Remove(string bluetoothAddress)
        {
            var deviceEntity = await _context.Device.FindAsync(bluetoothAddress);

            if (deviceEntity == null)
                return EntityState.Unchanged;

            var state = _context.Device.Remove(deviceEntity);

            await _context.SaveChangesAsync();

            return state.State;
        }
    }
}
