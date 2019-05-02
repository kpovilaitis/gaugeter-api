using System.Collections.Generic;
using System.Threading.Tasks;
using Gaugeter.Api.Users.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Users.Repository
{
    public interface IUsersRepository
    {
        Task<User> Get(string id);

        Task<IEnumerable<User>> GetAll();

        Task<EntityState> Create(User user);

        Task<EntityState> Update(User user);

        Task<EntityState> Delete(string id);
    }
}
