using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SnowLeopard.Infrastructure.Json;
using SnowLeopard.Lynx;

namespace SnowLeopard.Mongo
{
    /// <summary>
    /// MongoContext Extensions
    /// </summary>
    public static class MongoContextExtensions
    {
        /// <summary>
        /// AddSnowLeopardMongoContext
        /// </summary>
        /// <param name="services"></param>
        public static void AddSnowLeopardMongoContext(this IServiceCollection services)
        {
            JsonSerializerSetting.Config(x =>
            {
                x.Converters.Add(new ObjectIdConverter());
            });

            var types = LynxUtils.GetAllType();
            var singletonServiceInterface = typeof(MongoContext);
            var singletonServiceTypes = types.Where(x => singletonServiceInterface.IsAssignableFrom(x) && !x.GetTypeInfo().IsAbstract).ToArray();

            // 注册 Singleton 服务
            foreach (var serviceType in singletonServiceTypes)
            {
                services.AddSingleton(serviceType);
            }
        }

        /// <summary>
        /// AddSnowLeopardMongoContext
        /// </summary>
        /// <param name="services"></param>
        public static void AddSnowLeopardMongoContext<T>(this IServiceCollection services) where T : MongoContext
        {
            services.AddSingleton<T>();
        }
    }
}
