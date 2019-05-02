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

        public async Task<User> Get(string userId)
        {
            return await _usersRepository.Get(userId);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _usersRepository.GetAll();
        }

        public async Task<EntityState> Create(User user)
        {
            user.Password = _hashGenerator.ComputeSha1Hash(user.Password);

            return await _usersRepository.Create(user);
        }

        public async Task<EntityState> Update(User user)
        {
            user.Password = _hashGenerator.ComputeSha1Hash(user.Password);

            return await _usersRepository.Update(user);
        }

        public async Task<EntityState> Delete(string userId)
        { 
            return await _usersRepository.Delete(userId);
        }
    }
}
