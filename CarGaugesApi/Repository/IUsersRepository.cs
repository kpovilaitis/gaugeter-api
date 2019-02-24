using System.Collections.Generic;
using System.Threading.Tasks;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Repository
{
    public interface IUsersRepository
    {
        User GetUser(int id);

        User GetUser(string username, string password);

        List<User> GetAllUsers();

        EntityState CreateUser(User user);

        EntityState UpdateUser(User user);

        EntityState DeleteUser(int id);
    }
}
