using System.Threading.Tasks;
using CarGaugesApi.Authentication.Repository.AuthRepo;
using CarGaugesApi.Authentication.Services.TokenService;
using CarGaugesApi.Helpers.HashGenerator;
using CarGaugesApi.Models;

namespace CarGaugesApi.Authentication.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthRepository _authRepository;
        private readonly IHashGenerator _hashGenerator;

        public AuthService(ITokenService tokenService, IAuthRepository authRepository, IHashGenerator hashGenerator)
        {
            _tokenService = tokenService;
            _authRepository = authRepository;
            _hashGenerator = hashGenerator;
        }

        public async Task<User> Authenticate(string userId, string password)
        {
            var hashedPassword = _hashGenerator.ComputeSha1Hash(password);

            var user = await _authRepository.GetUser(userId, hashedPassword);

            if (user == null)
                return null;

            user.Token = await _tokenService.CreateToken(userId);
            user.Password = null;

            return user;
        }
    }
}
