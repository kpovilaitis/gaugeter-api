using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarGaugesApi.Data;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly CarGaugesDbContext _context;

        public UsersRepository(CarGaugesDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.User.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<EntityState> CreateUser(User user)
        {
            var state = _context.User.Add(user).State;
            await _context.SaveChangesAsync();
            return state;
        }

        public async Task UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.User.SingleOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return;
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUser(string username, string password)
        {
            return await _context.User.SingleOrDefaultAsync(x => x.Username == username && x.Password == password);
        }
    }
}
