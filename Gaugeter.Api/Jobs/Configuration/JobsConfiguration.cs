using Gaugeter.Api.Jobs.Repository;
using Gaugeter.Api.Jobs.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gaugeter.Api.Jobs.Configuration
{
    public static class JobsConfiguration
    {
        public static void AddJobsConfiguration(this IServiceCollection services)
        {
            services.AddTransient<IJobsService, JobsService>();
            services.AddTransient<IJobsRepository, JobsRepository>();
        }
    }
}