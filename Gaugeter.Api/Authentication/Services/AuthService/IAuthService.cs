using System.Threading.Tasks;
using Gaugeter.Api.Users.Models;

namespace Gaugeter.Api.Authentication.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> Authenticate(string email, string password);
    }
}
