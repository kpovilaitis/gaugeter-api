using System.Text;
using CarGaugesApi.Authentication;
using CarGaugesApi.Authentication.Configuration;
using CarGaugesApi.Constants;
using CarGaugesApi.Data;
using CarGaugesApi.Helpers;
using CarGaugesApi.Helpers.HashGenerator;
using CarGaugesApi.Repository;
using CarGaugesApi.Repository.DevicesRepo;
using CarGaugesApi.Repository.UsersRepo;
using CarGaugesApi.Services.Devices;
using CarGaugesApi.Services.Users;
using CarGaugesApi.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarGaugesApi
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
            services.AddMvc();

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IUsersRepository, UsersRepository>();
            //services.AddTransient<ITokenFactory, TokenFactory>();
            services.AddTransient<IDevicesService, DevicesService>();
            services.AddTransient<IDevicesRepository, DevicesRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IHashGenerator, HashGenerator>();

            services.AddGaugeterAuthentication();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.Configure<AuthenticationSettings>(Configuration.GetSection("Authentication"));

            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                //o.AssumeDefaultVersionWhenUnspecified = true;//Without this flag, the UnsupportedApiVersion exception will occur when the version is not specified by the client.
                o.DefaultApiVersion = new ApiVersion(AppVersions.APP_VERSION_APPLE, AppVersions.APP_VERSION_APPLE_MINOR);
            });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();

            services.AddDbContext<CarGaugesDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CarGaugesDbContext")));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
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
