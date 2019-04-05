using System.Threading.Tasks;
using CarGaugesApi.Models;

namespace CarGaugesApi.Authentication.Services.AuthService
{
    public interface IAuthService
    {
        Task<User> Authenticate(string email, string password);
    }
}
