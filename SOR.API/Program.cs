using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SOR.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                // Handle requests up to 50 MB
                options.Limits.MaxRequestBodySize = /*52428800*/ 737280000;
                    })
                    .UseStartup<Startup>();
                });
    }
}
