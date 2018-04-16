using Microsoft.Extensions.DependencyInjection;
using SnowLeopard.Abstractions.Services;
using System.Linq;
using System.Reflection;

namespace SnowLeopard.Extensions
{
    /// <summary>
    /// ServiceRegisterExtension
    /// </summary>
    public static class ServiceRegisterExtension
    {
        /// <summary>
        /// AddSnowLeopardServices
        /// </summary>
        /// <param name="services"></param>
        public static void AddSnowLeopardServices(this IServiceCollection services)
        {
            services.AddSingleton<SnowLeopardUtils>();

            #region 自动注册 IDependencyTransientRegister、IDependencyTransientRegister

            var utils = new SnowLeopardUtils();
            var assemblys = utils.GetAllAssembly();
            var types = utils.GetAllType();

            var transientServiceInterface = typeof(IDependencyTransientRegister);
            var scopedServiceInterface = typeof(IDependencyScopedRegister);

            var transientServiceTypes = types.Where(x => transientServiceInterface.IsAssignableFrom(x) && x.GetTypeInfo().IsAbstract).ToArray();
            var transientImplementationTypes = types.Where(x => transientServiceInterface.IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract).ToArray();

            var scopedServiceTypes = types.Where(x => scopedServiceInterface.IsAssignableFrom(x) && x.GetTypeInfo().IsAbstract).ToArray();
            var scopedImplementationTypes = types.Where(x => scopedServiceInterface.IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract).ToArray();

            // 注册 Transient 服务
            foreach (var serviceType in transientServiceTypes)
            {
                if (serviceType == transientServiceInterface || serviceType == scopedServiceInterface) continue;
                // 过滤实现类
                var implementationTypes = transientImplementationTypes.Where(x => serviceType.IsAssignableFrom(x));

                // 注册服务
                foreach (var implementationType in implementationTypes)
                {
                    services.AddTransient(serviceType, implementationType);
                }
            }

            // 注册 Scoped 服务
            foreach (var serviceType in scopedServiceTypes)
            {
                if (serviceType == transientServiceInterface || serviceType == scopedServiceInterface) continue;
                // 过滤实现类
                var implementationTypes = scopedImplementationTypes.Where(x => serviceType.IsAssignableFrom(x));

                // 注册服务
                foreach (var implementationType in implementationTypes)
                {
                    services.AddScoped(serviceType, implementationType);
                }
            }

            #endregion
        }
    }
}
