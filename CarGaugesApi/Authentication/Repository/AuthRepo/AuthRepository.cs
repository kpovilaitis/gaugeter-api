using System.Threading.Tasks;
using CarGaugesApi.Data;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Authentication.Repository.AuthRepo
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CarGaugesDbContext _context;

        public AuthRepository(CarGaugesDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string userId, string password)
        {
            return await _context.User.SingleOrDefaultAsync(x => x.UserId == userId && x.Password == password);
        }
    }
}
