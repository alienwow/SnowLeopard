using Consul;
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
        /// <summary>
        /// ResolveService
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public override async Task<CatalogService[]> ResolveServiceAsync(string serviceName)
        {
            CatalogService[] services;
            using (var consulClient = new ConsulClient(c => c.Address = new Uri(_consulServerUrl)))
            {
                services = (await consulClient.Catalog.Service(serviceName)).Response;
            }
            return services;
        }

        /// <summary>
        /// 地址转换
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public override async Task<string> ResolveUrlAsync(string url)
        {
            var uri = new Uri(url);
            string realRootUrl = await ResolveRootUrlAsync(uri.Host);
            return $"{uri.Scheme}://{realRootUrl}{uri.PathAndQuery}";
        }

        /// <summary>
        /// Host:Port转换
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private async Task<string> ResolveRootUrlAsync(string serviceName)
        {
            CatalogService[] services = await ResolveServiceAsync(serviceName);
            if (services.Length == 0)
            {
                throw new ArgumentException($"找不到服务 {serviceName} 的任何实例");
            }
            else
            {
                //根据当前时钟毫秒数对可用服务个数取模，取出一台机器使用
                var service = services.ElementAt(Environment.TickCount % services.Count());
                return $"{service.Address}:{service.ServicePort}";
            }
        }

    }
}
