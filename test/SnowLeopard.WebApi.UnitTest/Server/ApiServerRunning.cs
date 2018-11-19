using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace SnowLeopard.WebApi.UnitTest
{
    public class ApiServerRunning : IDisposable
    {
        private IWebHost _builder;

        public void Dispose()
        {
            _builder?.Dispose();
        }

        public void GivenRunningOn(string url)
        {
            _builder = new WebHostBuilder()
                   .UseUrls(url)
                   .UseKestrel()
                   .UseContentRoot(Directory.GetCurrentDirectory())
                   .UseStartup<Startup>()
                   .Build();
            _builder.Start();
        }
    }
}
