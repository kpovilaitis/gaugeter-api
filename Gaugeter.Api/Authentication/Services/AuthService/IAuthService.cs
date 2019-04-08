using System.Threading.Tasks;
using Gaugeter.Api.Authentication.Models.Data;

namespace Gaugeter.Api.Authentication.Services.AuthService
{
    public interface IAuthService
    {
        Task<Login> Authenticate(string email, string password);
    }
}
