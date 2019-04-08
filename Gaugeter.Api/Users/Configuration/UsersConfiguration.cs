using Gaugeter.Api.Users.Repository;
using Gaugeter.Api.Users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gaugeter.Api.Users.Configuration
{
    public static class UsersConfiguration
    {
        public static void AddUsersConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IUsersRepository, UsersRepository>();
        }
    }
}