using System;
using System.Threading.Tasks;
using Gaugeter.Api.Authentication.Models.Data;
using Gaugeter.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Authentication.Repository.TokenRepo
{
    public class TokenRepository : ITokenRepository
    {
        private readonly GaugeterDbContext _context;

        public TokenRepository(GaugeterDbContext context)
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
            return await _context.ActiveToken.FindAsync(token);
        }

        public async Task UpdateTokenExpiration(string token, DateTime expiration)
        {
            var activeTokenEntity = await _context.ActiveToken.FindAsync(token);

            if (activeTokenEntity != null)
                activeTokenEntity.ExpirationDate = expiration;

            await _context.SaveChangesAsync();
        }

        public async Task<EntityState> RemoveToken(string token)
        {
            var tokenEntity = await _context.ActiveToken.FindAsync(token);

            if (tokenEntity == null)
                return EntityState.Unchanged;

            _context.ActiveToken.Remove(tokenEntity);

            await _context.SaveChangesAsync();
            return EntityState.Deleted;
        }
    }
}
