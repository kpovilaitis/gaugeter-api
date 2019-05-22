using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Constants;
using Gaugeter.Api.Users.Models.Data;
using Gaugeter.Api.Users.Repository;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Tests.Users
{
    public class UsersRepositoryTest : IUsersRepository
    {
        private readonly User _userIngrida = new User
        {
            UserId = "Ingrida2",
            Password = "password",
            Description = "good gal2",
            Devices = null,
            MeasurementSystem = Enums.MEASUREMENT_SYSTEM.Metric
        };
        
        public async Task<User> Get(string id)
        {            
            return _userIngrida.UserId != id ? null : _userIngrida;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return new List<User>
            {
                _userIngrida,
                new User
                {
                    UserId = "Kestutis",
                    Password = "passwordMano",
                    Description = "good boi",
                    Devices = null,
                    MeasurementSystem = Enums.MEASUREMENT_SYSTEM.Imperial
                }
            };
        }
        
        public async Task<EntityState> Create(User user)
        { 
            return EntityState.Added;
        }

        public async Task<EntityState> Update(User user)
        {
            return user.UserId == _userIngrida.UserId ? EntityState.Modified : EntityState.Unchanged;
        }

        public async Task<EntityState> Delete(string id)
        {           
            return id == _userIngrida.UserId ? EntityState.Deleted : EntityState.Unchanged;
        }
    }
}
