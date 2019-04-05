using System;
using System.Threading.Tasks;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Authentication.Repository.TokenRepo
{
    public interface ITokenRepository
    {
        Task<ActiveToken> GetToken(string token);

        Task UpdateTokenExpiration(string token, DateTime expiration);

        Task CreateToken(ActiveToken token);

        Task<EntityState> RemoveToken(string token);
    }
}
