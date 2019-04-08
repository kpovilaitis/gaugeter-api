using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Users.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Users.Repository
{
    public interface IUsersRepository
    {
        Task<User> GetUser(string id);

        Task<IEnumerable<User>> GetAllUsers();

        Task<EntityState> CreateUser(User user);

        Task<EntityState> UpdateUser(User user);

        Task<EntityState> DeleteUser(string id);
    }
}
