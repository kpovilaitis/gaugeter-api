using System;
using System.Threading.Tasks;
using Gaugeter.Api.Authentication.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Authentication.Repository.TokenRepo
{
    public interface ITokenRepository
    {
        Task<ActiveToken> GetToken(string token);

        Task UpdateTokenExpiration(string token, DateTime expiration);

        Task CreateToken(ActiveToken token);

        Task<EntityState> RemoveToken(string token);
    }
}
