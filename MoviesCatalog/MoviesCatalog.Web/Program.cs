using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using MoviesCatalog.Services.Providers;
using System.Threading.Tasks;

namespace MoviesCatalog.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = BuildWebHost(args);
            await SeedData.SeedDatabase(host);
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
          WebHost.CreateDefaultBuilder(args)
              .UseStartup<Startup>()
              .Build();
    }
}
