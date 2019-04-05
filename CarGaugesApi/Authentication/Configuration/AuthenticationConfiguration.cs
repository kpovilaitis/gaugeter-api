using CarGaugesApi.Authentication.Handlers;
using CarGaugesApi.Authentication.Repository.AuthRepo;
using CarGaugesApi.Authentication.Repository.TokenRepo;
using CarGaugesApi.Authentication.Services.AuthService;
using CarGaugesApi.Authentication.Services.TokenService;
using CarGaugesApi.Authentication.Services.UserInfoAccessor;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace CarGaugesApi.Authentication.Configuration
{
    public static class AuthenticationConfiguration
    {
        public static void AddGaugeterAuthentication(this IServiceCollection services)
        {
            services.AddTransient<ITokenRepository, TokenRepository>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IUserInfoAccessor, UserInfoAccessor>();
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, GaugeterAuthenticationHandler>("BasicAuthentication", null);

        }
    }
}
