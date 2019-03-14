using System.Text;
using CarGaugesApi.Authentication;
using CarGaugesApi.Constants;
using CarGaugesApi.Data;
using CarGaugesApi.Helpers;
using CarGaugesApi.Repository;
using CarGaugesApi.Services.Users;
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
using Microsoft.IdentityModel.Tokens;

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
            services.AddTransient<ITokenFactory, TokenFactory>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true
                };
            });

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseResponseCompression();
            app.UseMvc();
        }
    }
}
