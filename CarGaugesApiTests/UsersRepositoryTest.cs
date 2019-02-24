using System.Collections.Generic;
using System.Threading.Tasks;
using CarGaugesApi.Models;
using CarGaugesApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApiTests
{
    public class UsersRepositoryTest : IUsersRepository
    {
        public User GetUser(int id)
        {
            var userIngrida = new User(3, "Ingrida2", "password", "good gal2", null);

            if (userIngrida.Id != id)
            {
                userIngrida = null;
            }
            return userIngrida;
        }

        public List<User> GetAllUsers()
        {
            var userIngrida = new User(3, "Ingrida2", "password", "good gal2", null);
            var userKestutis = new User(4, "Kestutis", "password", "good boi", null);
            var userList = new List<User>
            {
                userIngrida,
                userKestutis
            };
            return userList;
        }

        public EntityState CreateUser(User user)
        { 
            return EntityState.Added;
        }

        public EntityState UpdateUser(User user)
        {
            var userIngrida = new User(3, "Ingrida2", "password", "good gal2", null);

            if (user.Id == userIngrida.Id)
            {
                return EntityState.Modified;
            }
            else
            {
                return EntityState.Unchanged;
            }
        }

        public EntityState DeleteUser(int id)
        {
            var userIngrida = new User(3, "Ingrida2", "password", "good gal2", null);

            if (id == userIngrida.Id)
            {
                return EntityState.Deleted;
            }
            else
            {
                return EntityState.Unchanged;
            }
        }

        public User GetUser(string username, string password)
        {
            var userIngrida = new User(3, "Ingrida2", "password", "good gal2", null);

            if (userIngrida.Username != username && userIngrida.Password != password)
            {
                userIngrida = null;
            }
            return userIngrida;
        }
    }
}
