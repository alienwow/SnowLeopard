using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Reflection;

namespace SnowLeopard.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            if (args != null) configurationBuilder.AddCommandLine(args);

            var hostingConfig = configurationBuilder.Build();
            var globalConfig = hostingConfig.Get<GlobalConfig>();

            return WebHost
                    .CreateDefaultBuilder(args)
                    .UseKestrel()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureAppConfiguration((hostingCtx, config) =>
                    {
                        var env = hostingCtx.HostingEnvironment;

                        if (env.IsDevelopment())
                        {
                            var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                            if (appAssembly != null)
                            {
                                config.AddUserSecrets(appAssembly, optional: true);
                            }
                        }
                    })
                    .ConfigureLogging((hostingCtx, logging) =>
                    {
                        logging.AddConfiguration(hostingCtx.Configuration.GetSection("Logging"));
                        logging.AddConsole();
                        logging.AddDebug();
                    })
                    .UseUrls(globalConfig.ApplicationUrl)
                    .UseStartup<Startup>();
        }
    }
}
