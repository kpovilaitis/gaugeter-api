using System;
using System.Threading.Tasks;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Authentication.Services.TokenService
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
