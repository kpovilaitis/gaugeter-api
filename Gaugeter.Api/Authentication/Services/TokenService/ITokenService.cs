using System;
using System.Threading.Tasks;
using Gaugeter.Api.Authentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Authentication.Services.TokenService
{
    public interface ITokenService
    {
        Task<ActiveToken> GetToken(string token);
        
        Task UpdateTokenExpiration(string token, DateTime expiration);

        Task<string> CreateToken(string userId);

        Task<EntityState> RemoveToken(string token);

        string CreateRefreshToken();
    }
}
