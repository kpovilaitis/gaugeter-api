using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Users.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Users.Services
{
    public interface IUsersService
    {
        Task<User> Get(string userId);

        Task<IEnumerable<User>> GetAll();

        Task<EntityState> Create(User user);

        Task<EntityState> Update(User user);

        Task<EntityState> Delete(string userId);
    }
}
