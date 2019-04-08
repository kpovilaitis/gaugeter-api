using System.Threading.Tasks;
using Gaugeter.Api.Data;
using Gaugeter.Api.Users.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Authentication.Repository.AuthRepo
{
    public class AuthRepository : IAuthRepository
    {
        private readonly GaugeterDbContext _context;

        public AuthRepository(GaugeterDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUser(string userId, string password)
        {
            try
            {
                return await _context.User.SingleAsync(x => x.UserId == userId && x.Password == password);
            } 
            catch
            {
                return null;
            }
        }
    }
}
