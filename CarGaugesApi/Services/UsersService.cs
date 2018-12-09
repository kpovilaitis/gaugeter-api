using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CarGaugesApi.Helpers;
using CarGaugesApi.Models;
using CarGaugesApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarGaugesApi.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly AppSettings _appSettings;

        public UsersService(IUsersRepository usersRepository, IOptions<AppSettings> appSettings)
        {
            _usersRepository = usersRepository;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _usersRepository.GetUser(username, password).Result;

            // return null if user not found
            if (user == null)
                return null;

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            user.Token = tokenHandler.WriteToken(token);

            // remove password before returning
            user.Password = null;

            return user;
        }

        public async Task<User> GetUser(int id)
        {
            return await _usersRepository.GetUser(id);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersRepository.GetAllUsers();
        }

        public async Task<EntityState> CreateUser(User user)
        {
            return await _usersRepository.CreateUser(user);
        }

        public async Task UpdateUser(User user)
        {
            await _usersRepository.UpdateUser(user);
        }

            public async Task DeleteUser(int id)
            {
                await _usersRepository.DeleteUser(id);
            }
    }
}
