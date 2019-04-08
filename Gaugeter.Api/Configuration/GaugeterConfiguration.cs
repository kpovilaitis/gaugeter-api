using Gaugeter.Api.Data;
using Gaugeter.Api.Constants;
using Gaugeter.Api.Helpers;
using Gaugeter.Api.Helpers.HashGenerator;
using Gaugeter.Api.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Gaugeter.Api.Configuration
{
    public static class GaugeterConfiguration
    {
        public static void AddGaugeterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IHashGenerator, HashGenerator>();

            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
            services.Configure<AuthenticationSettings>(configuration.GetSection("Authentication"));

            services.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.ApiVersionReader = new HeaderApiVersionReader("api-version");
                //o.AssumeDefaultVersionWhenUnspecified = true;//Without this flag, the UnsupportedApiVersion exception will occur when the version is not specified by the client.
                o.DefaultApiVersion = new ApiVersion(AppVersions.APP_VERSION_APPLE, AppVersions.APP_VERSION_APPLE_MINOR);
            });

            services.Configure<GzipCompressionProviderOptions>(options => options.Level = System.IO.Compression.CompressionLevel.Optimal);
            services.AddResponseCompression();

            services.AddDbContext<CarGaugesDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("GaugeterDbContext")));
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }
    }
}
