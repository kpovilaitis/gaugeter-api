using System.Collections.Generic;
using System.Threading.Tasks;
using CarGaugesApi.Models;
using CarGaugesApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApiTests
{
    public class UsersRepositoryTest : IUsersRepository
    {
        public async Task<User> GetUser(int id)
        {
            var userIngrida = new User(3, "Ingrida2", "password", "good gal2", null);

            if (userIngrida.Id != id)
            {
                userIngrida = null;
            }
            return await new Task<User>(() => userIngrida);
        }

        public async Task<List<User>> GetAllUsers()
        {
            var userIngrida = new User(3, "Ingrida2", "password", "good gal2", null);
            var userKestutis = new User(4, "Kestutis", "password", "good boi", null);
            var userList = new List<User>
            {
                userIngrida,
                userKestutis
            };
            return await new Task<List<User>>(() => userList);
        }

        public async Task<EntityState> CreateUser(User user)
        {
            var userIngrida = new User(3, "Ingrida2", "password", "good gal2", null);

            if (user == userIngrida)
            {
                return await new Task<EntityState>(() => EntityState.Added);
            }
            else
            {
                return await new Task<EntityState>(() => EntityState.Unchanged);
            }
        }

        public async Task UpdateUser(User user)
        {
            var userIngrida = new User(3, "Ingrida2", "password", "good gal2", null);

            if (user.Id == userIngrida.Id)
            {
                await new Task<EntityState>(() => EntityState.Modified);
            }
            else
            {
                await new Task<EntityState>(() => EntityState.Unchanged);
            }
        }

        public async Task DeleteUser(int id)
        {
            var userIngrida = new User(3, "Ingrida2", "password", "good gal2", null);

            if (id == userIngrida.Id)
            {
                await new Task<EntityState>(() => EntityState.Deleted);
            }
            else
            {
                await new Task<EntityState>(() => EntityState.Unchanged);
            }
        }

        public async Task<User> GetUser(string username, string password)
        {
            var userIngrida = new User(3, "Ingrida2", "password", "good gal2", null);

            if (userIngrida.Username != username && userIngrida.Password != password)
            {
                userIngrida = null;
            }
            return await new Task<User>(() => userIngrida);
        }
    }
}
