using System.Threading.Tasks;
using CarGaugesApi.Models;

namespace CarGaugesApi.Authentication.Repository.AuthRepo
{
    public interface IAuthRepository
    {
        Task<User> GetUser(string userId, string password);
    }
}
