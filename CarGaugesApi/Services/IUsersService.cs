using System.Collections.Generic;
using System.Threading.Tasks;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Services
{
    public interface IUsersService
    {
        User Authenticate(string username, string password);

        Task<User> GetUser(int id);

        Task<List<User>> GetAllUsers();

        Task<EntityState> CreateUser(User user);

        Task UpdateUser(User user);

        Task DeleteUser(int id);
    }
}
