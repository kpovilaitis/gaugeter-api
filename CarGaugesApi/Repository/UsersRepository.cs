using System.Collections.Generic;
using System.Linq;
using CarGaugesApi.Data;
using CarGaugesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarGaugesApi.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly CarGaugesDbContext _context;

        public UsersRepository(CarGaugesDbContext context)
        {
            _context = context;
        }

        public User GetUser(int id)
        {
            return _context.User.SingleOrDefault(m => m.Id == id);
        }

        public List<User> GetAllUsers()
        {
            return _context.User.ToList();
        }

        public EntityState CreateUser(User user)
        {
            var state = _context.User.Add(user).State;
            _context.SaveChanges();
            return state;
        }

        public EntityState UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;

            _context.SaveChanges();

            return _context.Entry(user).State;
        }

        public EntityState DeleteUser(int id)
        {
            var user = _context.User.SingleOrDefault(m => m.Id == id);

            if (user == null)
            {
                return EntityState.Unchanged;
            }

            _context.User.Remove(user);
            _context.SaveChanges();

            return EntityState.Modified;
        }

        public User GetUser(string username, string password)
        {
            return _context.User.SingleOrDefault(x => x.Username == username && x.Password == password);
        }
    }
}
