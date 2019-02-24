using System.Collections.Generic;
using System.Threading.Tasks;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Services
{
    public interface IUsersService
    {
        User Authenticate(string username, string password);

        User GetUser(int id);

        List<User> GetAllUsers();

        EntityState CreateUser(User user);

        EntityState UpdateUser(User user);

        EntityState DeleteUser(int id);
    }
}
