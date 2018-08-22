using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using SnowLeopard.Lynx.Extension;
using System;
using System.IO;
using System.Text;

namespace SnowLeopard.DependencyInjection
{
    /// <summary>
    /// 注册服务
    /// </summary>
    public static class ServiceRegister
    {
        internal const string _CONSUL_SERVER_URL = "http://127.0.0.1:8500";
        internal const string _CONSUL_DEFAULT_DC = "dc1";
        internal const string _SERVICE_ID_FILE = "service-id";

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="self"></param>
        /// <param name="serviceName"></param>
        /// <param name="applicationUrl"></param>
        /// <param name="applicationLifetime"></param>
        /// <param name="serviceId"></param>
        /// <param name="configOverride"></param>
        public static void RegisterService(
            this IApplicationBuilder self,
            string serviceName,
            string applicationUrl,
            IApplicationLifetime applicationLifetime = null,
            string serviceId = null,
            Action<ConsulClientConfiguration> configOverride = null
        )
        {
            if (serviceName.IsMissing())
                throw new ArgumentNullException(nameof(serviceName));

            if (applicationLifetime == null)
                applicationLifetime = GlobalServices.GetRequiredService<IApplicationLifetime>();

            var host = new Uri(applicationUrl);

            using (var fs = new FileStream(_SERVICE_ID_FILE, FileMode.OpenOrCreate))
            {
                if (serviceId.IsMissing() && fs.Length > 0)
                {
                    var bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, bytes.Length);
                    serviceId = Encoding.UTF8.GetString(bytes);
                }
                else
                {
                    serviceId = $"{serviceName}-{Guid.NewGuid()}";
                    var bytes = Encoding.UTF8.GetBytes(serviceId);
                    fs.Write(bytes, 0, bytes.Length);
                }
            }

            if (configOverride == null)
                configOverride = ConsulConfig;

            // 注册服务到 Consul
            using (var client = new ConsulClient(configOverride))
            {
                var result = client.Agent.ServiceRegister(new AgentServiceRegistration()
                {
                    ID = serviceId,
                    Name = serviceName,
                    Address = host.Host,
                    Port = host.Port,
                    Check = new AgentServiceCheck
                    {
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(5), //服务停止多久 后反注册(注销)
                        HTTP = $"http://{host.Host}:{host.Port}/api/v1/health",//健康检查地址 
                        Interval = TimeSpan.FromSeconds(5),//健康检查时间间隔，或者称为心跳 间隔
                        Timeout = TimeSpan.FromSeconds(5),
                    }
                }).Result;
            }

            // 注销服务
            //程序正常退出的时候从 Consul 注销服务
            //要通过方法参数注入 IApplicationLifetime
            applicationLifetime.ApplicationStopped.Register(() =>
            {
                using (var client = new ConsulClient(configOverride))
                {
                    var result = client.Agent.ServiceDeregister(serviceId).Result;
                }
            });
        }

        private static void ConsulConfig(ConsulClientConfiguration obj)
        {
            obj.Address = new Uri(_CONSUL_SERVER_URL);
            obj.Datacenter = _CONSUL_DEFAULT_DC;
        }

    }
}
