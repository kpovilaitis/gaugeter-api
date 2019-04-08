using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Users.Repository
{
    public interface IUsersRepository
    {
        Task<User> GetUser(string id);

        Task<List<User>> GetAllUsers();

        Task<EntityState> CreateUser(User user);

        Task<EntityState> UpdateUser(User user);

        Task<EntityState> DeleteUser(string id);
    }
}
