using Gaugeter.Api.Authentication.Handlers;
using Gaugeter.Api.Authentication.Repository.AuthRepo;
using Gaugeter.Api.Authentication.Repository.TokenRepo;
using Gaugeter.Api.Authentication.Services.AuthService;
using Gaugeter.Api.Authentication.Services.TokenService;
using Gaugeter.Api.Authentication.Services.UserInfoAccessor;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Gaugeter.Api.Authentication.Configuration
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
