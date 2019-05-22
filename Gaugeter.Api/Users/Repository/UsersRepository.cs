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

        public async Task<User> Get(string userId)
        {
            return await _context.User.FindAsync(userId);
        }
        
        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<EntityState> Create(User user)
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

        public async Task<EntityState> Update(User user)
        {
            var userEntity = await _context.User.FindAsync(user.UserId);

            if (userEntity == null)
                return EntityState.Unchanged;

            userEntity.Password = user.Password;
            userEntity.Description = user.Description;
            userEntity.MeasurementSystem = user.MeasurementSystem;
            
            _context.Entry(userEntity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return EntityState.Modified;
        }

        public async Task<EntityState> Delete(string userId)
        {
            var userEntity = await _context.User.FindAsync(userId);

            if (userEntity == null)
                return EntityState.Unchanged;

            var state = _context.User.Remove(userEntity).State;

            await _context.SaveChangesAsync();

            return state;
        }
    }
}
