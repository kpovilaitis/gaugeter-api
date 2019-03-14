using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace CarGaugesApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("https://192.168.1.226:5001")
                .UseSetting("https_port", "443")
                .UseStartup<Startup>();
    }
}
