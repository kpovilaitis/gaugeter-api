using Gaugeter.Api.Devices.Repository;
using Gaugeter.Api.Services.Devices;
using Microsoft.Extensions.DependencyInjection;

namespace Gaugeter.Api.Devices.Configuration
{
    public static class DevicesConfiguration
    {
        public static void AddDevicesConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IDevicesService, DevicesService>();
            services.AddTransient<IDevicesRepository, DevicesRepository>();
        }
    }
}
