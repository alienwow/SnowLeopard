using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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
                .AddJsonFile("appsettings.Development.json", true, false)
                .AddJsonFile("appsettings.Production.json", true, false)
                .AddEnvironmentVariables()
                .AddCommandLine(args);

            if (args != null)
            {
                configurationBuilder.AddCommandLine(args);
            }
            var hostingconfig = configurationBuilder.Build();
            var url = hostingconfig[GlobalConsts.APPLICATION_URL_KEY];
            var globalConfig = hostingconfig.Get<GlobalConfig>();

            return
                WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((builderContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", false, true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, builder) =>
                {
                    builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .UseConfiguration(hostingconfig)
                .UseStartup<Startup>()
                .UseKestrel()
                .UseUrls(url);
        }
    }
}
