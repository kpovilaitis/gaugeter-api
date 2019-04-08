using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Helpers.HashGenerator;
using Gaugeter.Api.Users.Models.Data;
using Gaugeter.Api.Users.Repository;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IHashGenerator _hashGenerator;

        public UsersService(IUsersRepository usersRepository, IHashGenerator hashGenerator)
        {
            _usersRepository = usersRepository;
            _hashGenerator = hashGenerator;
        }

        public async Task<User> GetUser(string userId)
        {
            return await _usersRepository.GetUser(userId);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _usersRepository.GetAllUsers();
        }

        public async Task<EntityState> CreateUser(User user)
        {
            user.Password = _hashGenerator.ComputeSha1Hash(user.Password);

            return await _usersRepository.CreateUser(user);
        }

        public async Task<EntityState> UpdateUser(User user)
        {
            user.Password = _hashGenerator.ComputeSha1Hash(user.Password);

            return await _usersRepository.UpdateUser(user);
        }

        public async Task<EntityState> DeleteUser(string userId)
        { 
            return await _usersRepository.DeleteUser(userId);
        }
    }
}
