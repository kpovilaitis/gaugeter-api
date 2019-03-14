using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarGaugesApi.Authentication;
using CarGaugesApi.Helpers;
using CarGaugesApi.Models;
using CarGaugesApi.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CarGaugesApi.Services.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ITokenFactory _tokenFactory;
        private readonly AppSettings _appSettings;

        public UsersService(IUsersRepository usersRepository, ITokenFactory tokenFactory, IOptions<AppSettings> appSettings)
        {
            _usersRepository = usersRepository;
            _tokenFactory = tokenFactory;
            _appSettings = appSettings.Value;
        }

        public User Authenticate(string username, string password)
        {
            var user = _usersRepository.GetUser(username, password);

            if (user == null)
                return null;

            //// authentication successful so generate jwt token
            //var tokenHandler = new JwtSecurityTokenHandler();
            //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            //var tokenDescriptor = new SecurityTokenDescriptor
            //{
            //    Subject = new ClaimsIdentity(new Claim[]
            //    {
            //        new Claim(ClaimTypes.Name, user.Id.ToString())
            //    }),
            //    Expires = DateTime.UtcNow.AddDays(7),
            //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            //};
            //var token = tokenHandler.CreateToken(tokenDescriptor);

            user.Token = _tokenFactory.GetAuthToken(user.Id.ToString(), _appSettings.Secret);
            user.RefreshToken = _tokenFactory.GetRefreshToken();
            // remove password before returning
            user.Password = null;

            return user;
        }

        public User GetUser(int id)
        {
            return _usersRepository.GetUser(id);
        }

        public List<User> GetAllUsers()
        {
            return _usersRepository.GetAllUsers();
        }

        public EntityState CreateUser(User user)
        {
            return _usersRepository.CreateUser(user);
        }

        public EntityState UpdateUser(User user)
        {
            return _usersRepository.UpdateUser(user);
        }

        public EntityState DeleteUser(int id)
        { 
            return _usersRepository.DeleteUser(id);
        }
    }
}
