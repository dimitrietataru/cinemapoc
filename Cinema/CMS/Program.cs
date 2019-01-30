using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace CMS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
                    {
                        var environment = webHostBuilderContext.HostingEnvironment;
                        configurationBuilder
                            .SetBasePath(environment.ContentRootPath)
                            .AddJsonFile("appSettings.json", true)
                            .AddJsonFile($"appSettings.{environment.EnvironmentName}.json", true);
                    })
                .UseStartup<Startup>()
                .Build();
    }
}
