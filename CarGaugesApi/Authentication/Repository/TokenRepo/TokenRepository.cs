using System;
using System.Threading.Tasks;
using CarGaugesApi.Data;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Authentication.Repository.TokenRepo
{
    public class TokenRepository : ITokenRepository
    {
        private readonly CarGaugesDbContext _context;

        public TokenRepository(CarGaugesDbContext context)
        {
            _context = context;
        }

        public async Task CreateToken(ActiveToken token)
        {
            await _context.ActiveToken.AddAsync(token);
            await _context.SaveChangesAsync();
        }

        public async Task<ActiveToken> GetToken(string token)
        {
            var activeToken = await _context.ActiveToken.SingleOrDefaultAsync(at => at.Token == token);
            return activeToken;
        }

        public async Task UpdateTokenExpiration(string token, DateTime expiration)
        {
            var activeToken = await _context.ActiveToken.SingleOrDefaultAsync(at => at.Token == token);

            activeToken.ExpirationDate = expiration;

            await _context.SaveChangesAsync();
        }

        public async Task<EntityState> RemoveToken(string token)
        {
            var foundToken = await _context.ActiveToken.SingleOrDefaultAsync(at => at.Token == token);

            if (foundToken == null)
                return EntityState.Unchanged;

            _context.ActiveToken.Remove(foundToken);

            await _context.SaveChangesAsync();
            return EntityState.Deleted;
        }
    }
}
