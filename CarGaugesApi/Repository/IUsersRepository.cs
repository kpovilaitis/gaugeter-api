using System.Collections.Generic;
using System.Threading.Tasks;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Repository
{
    public interface IUsersRepository
    {
        Task<User> GetUser(int id);

        Task<User> GetUser(string username, string password);

        Task<List<User>> GetAllUsers();

        Task<EntityState> CreateUser(User user);

        Task UpdateUser(User user);

        Task DeleteUser(int id);
    }
}
