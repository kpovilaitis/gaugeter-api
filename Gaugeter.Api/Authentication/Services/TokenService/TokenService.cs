using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Gaugeter.Api.Authentication.Models;
using Gaugeter.Api.Authentication.Repository.TokenRepo;
using Gaugeter.Api.Helpers.HashGenerator;
using Gaugeter.Api.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Gaugeter.Api.Authentication.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IHashGenerator _hashGenerator;
        private readonly AuthenticationSettings _apiSettings;

        public TokenService(ITokenRepository tokensRepository, IHashGenerator hashGenerator, IOptionsMonitor<AuthenticationSettings> apiSettings)
        {
            _tokenRepository = tokensRepository;
            _hashGenerator = hashGenerator;
            _apiSettings = apiSettings.CurrentValue;
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public async Task<string> CreateToken(string userId)
        {
            var token = _hashGenerator.ComputeSha256Hash(userId + DateTime.Now.Ticks.ToString());

            await _tokenRepository.CreateToken(new ActiveToken
            {
                 UserId = userId,
                 Token = $"Bearer {token}",
                 ExpirationDate = DateTime.Now.AddHours(_apiSettings.TokenValidHours)
            });

            return token;
        }

        public async Task<ActiveToken> GetToken(string token)
        {
            return await _tokenRepository.GetToken(token);
        }

        public async Task UpdateTokenExpiration(string token, DateTime expiration)
        {
            await _tokenRepository.UpdateTokenExpiration(token, expiration);
        }

        public async Task<EntityState> RemoveToken(string token)
        {
            return await _tokenRepository.RemoveToken(token);
        }
    }
}
