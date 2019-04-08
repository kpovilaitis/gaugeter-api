using System.Threading.Tasks;
using Gaugeter.Api.Users.Models.Data;

namespace Gaugeter.Api.Authentication.Repository.AuthRepo
{
    public interface IAuthRepository
    {
        Task<User> GetUser(string userId, string password);
    }
}
