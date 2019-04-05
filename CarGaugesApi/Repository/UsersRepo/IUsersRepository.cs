using System.Collections.Generic;
using System.Threading.Tasks;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Repository.UsersRepo
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
