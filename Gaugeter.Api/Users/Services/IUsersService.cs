using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Users.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Users.Services
{
    public interface IUsersService
    {
        Task<User> GetUser(string userId);

        Task<IEnumerable<User>> GetAllUsers();

        Task<EntityState> CreateUser(User user);

        Task<EntityState> UpdateUser(User user);

        Task<EntityState> DeleteUser(string userId);
    }
}
