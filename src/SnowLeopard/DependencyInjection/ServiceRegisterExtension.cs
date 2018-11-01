using Microsoft.Extensions.DependencyInjection;
using SnowLeopard.Abstractions.Services;
using SnowLeopard.Lynx;
using System.Linq;
using System.Reflection;

namespace SnowLeopard.DependencyInjection
{
    /// <summary>
    /// ServiceRegister Extension
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
            services.AddSingleton<CommonUtils>();

            #region 自动注册 IDependencyTransientRegister、IDependencyTransientRegister、IDependencySingletonRegister

            var types = LynxUtils.GetAllType();

            var transientServiceInterface = typeof(IDependencyTransientRegister);
            var scopedServiceInterface = typeof(IDependencyScopedRegister);
            var singletonServiceInterface = typeof(IDependencySingletonRegister);

            var transientServiceTypes = types.Where(x => transientServiceInterface.IsAssignableFrom(x) && x.GetTypeInfo().IsAbstract).ToArray();
            var transientImplementationTypes = types.Where(x => transientServiceInterface.IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract).ToArray();

            var scopedServiceTypes = types.Where(x => scopedServiceInterface.IsAssignableFrom(x) && x.GetTypeInfo().IsAbstract).ToArray();
            var scopedImplementationTypes = types.Where(x => scopedServiceInterface.IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract).ToArray();

            var singletonServiceTypes = types.Where(x => singletonServiceInterface.IsAssignableFrom(x)).ToArray();
            var singletonImplementationTypes = types.Where(x => singletonServiceInterface.IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract).ToArray();

            // 注册 Transient 服务
            foreach (var serviceType in transientServiceTypes)
            {
                if (serviceType == transientServiceInterface || serviceType == scopedServiceInterface || serviceType == singletonServiceInterface) continue;
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
                if (serviceType == transientServiceInterface || serviceType == scopedServiceInterface || serviceType == singletonServiceInterface) continue;
                // 过滤实现类
                var implementationTypes = scopedImplementationTypes.Where(x => serviceType.IsAssignableFrom(x));

                // 注册服务
                foreach (var implementationType in implementationTypes)
                {
                    services.AddScoped(serviceType, implementationType);
                }
            }

            // 注册 Singleton 服务
            foreach (var serviceType in singletonServiceTypes)
            {
                if (serviceType == transientServiceInterface || serviceType == scopedServiceInterface || serviceType == singletonServiceInterface) continue;
                // 过滤实现类
                var implementationTypes = singletonImplementationTypes.Where(x => serviceType.IsAssignableFrom(x));

                if (implementationTypes == null)
                {
                    // 注册服务
                    if (!serviceType.GetTypeInfo().IsAbstract)
                        services.AddSingleton(serviceType);
                }
                else
                {
                    // 注册服务
                    foreach (var implementationType in implementationTypes)
                    {
                        services.AddSingleton(serviceType, implementationType);
                    }
                }
            }

            #endregion
        }
    }
}
