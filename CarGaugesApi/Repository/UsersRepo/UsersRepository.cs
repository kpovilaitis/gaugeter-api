using System.Collections.Generic;
using System.Threading.Tasks;
using CarGaugesApi.Data;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Repository.UsersRepo
{
    public class UsersRepository : IUsersRepository
    {
        private readonly CarGaugesDbContext _context;

        public UsersRepository(CarGaugesDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string id)
        {
            return await _context.User.SingleOrDefaultAsync(m => m.UserId == id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<EntityState> CreateUser(User user)
        {
            var state = await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return state.State;
        }

        public async Task<EntityState> UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            var state = _context.Entry(user).State;
            await _context.SaveChangesAsync();
            return state;
        }

        public async Task<EntityState> DeleteUser(string id)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.UserId == id);

            if (user == null)
                return EntityState.Unchanged;

            _context.User.Remove(user);

            await _context.SaveChangesAsync();

            return EntityState.Modified;
        }
    }
}
