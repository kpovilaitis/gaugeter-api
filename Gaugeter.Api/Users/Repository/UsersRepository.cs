using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gaugeter.Api.Data;
using Gaugeter.Api.Users.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Users.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly GaugeterDbContext _context;

        public UsersRepository(GaugeterDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string userId)
        {
            return await _context.User.FindAsync(userId);
        }
        
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.User
                //.Include(u => u.Devices)
                .ToListAsync();
        }

        public async Task<EntityState> CreateUser(User user)
        {
            try
            {
                var userEntityState = _context.Entry(user).State = EntityState.Added;

                await _context.SaveChangesAsync();

                return userEntityState;
            }
            catch
            {
                return EntityState.Unchanged;
            }
        }

        public async Task<EntityState> UpdateUser(User user)
        {
            var userEntity = await _context.User.FindAsync(user.UserId);

            if (userEntity == null)
                return EntityState.Unchanged;

            userEntity = user;

            var entity = _context.Entry(userEntity);

            await _context.SaveChangesAsync();

            return entity.State;
        }

        public async Task<EntityState> DeleteUser(string userId)
        {
            var userEntity = await _context.User.FindAsync(userId);

            if (userEntity == null)
                return EntityState.Unchanged;

            var state = _context.User.Remove(userEntity);

            await _context.SaveChangesAsync();

            return state.State;
        }
    }
}
