using Gaugeter.Api.Authentication.Configuration;
using Gaugeter.Api.Configuration;
using Gaugeter.Api.Devices.Configuration;
using Gaugeter.Api.Users.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gaugeter.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGaugeterConfiguration(Configuration);
            services.AddUsersConfiguration();
            services.AddDevicesConfiguration();
            services.AddGaugeterAuthentication();

        }        

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseResponseCompression();
            app.UseMvc();
        }
    }
}
