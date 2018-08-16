using Consul;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure.Consul
{
    /// <summary>
    /// ServiceDiscovery
    /// </summary>
    public class DefaultServiceDiscovery : ServiceDiscovery
    {
        private readonly ILogger _logger;
        /// <summary>
        /// DefaultServiceDiscovery
        /// </summary>
        public DefaultServiceDiscovery()
        {
            _logger = GlobalServices.GetRequiredService<ILogger<DefaultServiceDiscovery>>();
        }

        /// <summary>
        /// DefaultServiceDiscovery
        /// </summary>
        /// <param name="consulServerUrl"></param>
        public DefaultServiceDiscovery(string consulServerUrl) : base(consulServerUrl)
        {
            _logger = GlobalServices.GetRequiredService<ILogger<DefaultServiceDiscovery>>();
        }

        /// <summary>
        /// ResolveService
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public override async Task<CatalogService[]> ResolveServiceAsync(string serviceName)
        {
            _logger.LogInformation($"ResolveServiceName:{serviceName}");
            using (var consulClient = new ConsulClient(c => c.Address = new Uri(_consulServerUrl)))
            {
                return (await consulClient.Catalog.Service(serviceName)).Response;
            }
        }

        /// <summary>
        /// 地址转换
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public override async Task<string> ResolveUrlAsync(string url)
        {
            _logger.LogInformation($"ResolveUrl:{url}");
            var uri = new Uri(url);
            string realRootUrl = await ResolveRootUrlAsync(uri.Host);
            _logger.LogInformation($"ResolveUrlResult:【{uri.Scheme}://{realRootUrl}{uri.PathAndQuery}】");
            return $"{uri.Scheme}://{realRootUrl}{uri.PathAndQuery}";
        }

        /// <summary>
        /// Host:Port转换
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private async Task<string> ResolveRootUrlAsync(string serviceName)
        {
            _logger.LogInformation($"ResolveRootUrlServiceName:{serviceName}");
            CatalogService[] services = await ResolveServiceAsync(serviceName);
            if (services.Length == 0)
            {
                _logger.LogWarning($"找不到服务 {serviceName} 的任何实例");
                throw new ArgumentException($"找不到服务 {serviceName} 的任何实例");
            }
            else
            {
                //根据当前时钟毫秒数对可用服务个数取模，取出一台机器使用
                var tickCount = Environment.TickCount;
                var index = tickCount % services.Count();
                _logger.LogDebug($"TickCount:{tickCount}\tservicesCount:{services.Count()}\tRemainderResult:{index}");
                var service = services.ElementAt(index);
                _logger.LogInformation($"ResolveRootUrlResult:【{service.ServiceAddress}:{service.ServicePort}】");
                return $"{service.ServiceAddress}:{service.ServicePort}";
            }
        }

    }
}
