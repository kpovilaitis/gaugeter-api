using System.Threading.Tasks;
using Gaugeter.Api.Users.Models;

namespace CarGaGaugeter.ApiugesApi.Authentication.Repository.AuthRepo
{
    public interface IAuthRepository
    {
        Task<User> GetUser(string userId, string password);
    }
}
