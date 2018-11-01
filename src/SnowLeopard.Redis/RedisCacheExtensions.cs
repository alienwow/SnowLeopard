using System;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SnowLeopard.Redis;
using StackExchange.Redis;

namespace SnowLeopard
{
    /// <summary>
    /// RedisCacheExtensions
    /// </summary>
    public static class RedisCacheExtensions
    {
        /// <summary>
        /// AddSnowLeopardRedis
        /// </summary>
        /// <param name="services"></param>
        /// <param name="Configuration"></param>
        public static void AddSnowLeopardRedis(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<RedisConfig>(Configuration);

            services.AddSingleton(x =>
            {
                var redisOption = x.GetRequiredService<IOptions<RedisConfig>>().Value.RedisOption;

                var connectionMultiplexer =
                    ConnectionMultiplexer.Connect(
                        string.IsNullOrEmpty(redisOption.ConnectionString) ?
                            RedisCache.DEFAULT_CONNECTIONSTRING :
                            redisOption.ConnectionString
                    );

                // 注册事件
                _registerEvents(connectionMultiplexer);

                return connectionMultiplexer;
            });

            services.AddSingleton<IRedisCache, RedisCache>();
        }

        /// <summary>
        /// AddSnowLeopardRedis
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSnowLeopardRedis(this IServiceCollection services, string configuration)
        {
            if (string.IsNullOrEmpty(configuration))
                throw new ArgumentNullException(nameof(configuration));

            services.AddSingleton(x =>
            {
                var connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);

                // 注册事件
                _registerEvents(connectionMultiplexer);

                return connectionMultiplexer;
            });
            services.AddSingleton<IRedisCache, RedisCache>();
        }

        /// <summary>
        /// AddSnowLeopardRedis
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddSnowLeopardRedis(this IServiceCollection services, ConfigurationOptions configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            services.AddSingleton(x =>
            {
                var connectionMultiplexer = ConnectionMultiplexer.Connect(configuration);

                // 注册事件
                _registerEvents(connectionMultiplexer);

                return connectionMultiplexer;
            });
            services.AddSingleton<IRedisCache, RedisCache>();
        }

        #region RedisEvents

        /// <summary>
        /// 注册事件
        /// </summary>
        /// <param name="connectionMultiplexer"></param>
        private static void _registerEvents(ConnectionMultiplexer connectionMultiplexer)
        {
            connectionMultiplexer.ConfigurationChanged += _redisConfigurationChanged;
            connectionMultiplexer.ConfigurationChangedBroadcast += _redisConfigurationChangedBroadcast;
            connectionMultiplexer.ConnectionRestored += _redisConnectionRestored;
            connectionMultiplexer.ConnectionFailed += _redisConnectionFailed;
            connectionMultiplexer.ErrorMessage += _redisErrorMessage;
            connectionMultiplexer.HashSlotMoved += _redisHashSlotMoved;
            connectionMultiplexer.InternalError += _redisInternalError;
        }

        /// <summary>
        /// 重新建立连接之前的错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void _redisConnectionRestored(object sender, ConnectionFailedEventArgs e)
        {
            Debug.WriteLine($"RedisConnectionRestored:{e.EndPoint}");
        }

        /// <summary>
        /// 通过发布订阅更新配置时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void _redisConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        {
            Debug.WriteLine($"RedisConfigurationChangedBroadcast:{e.EndPoint}");
        }

        /// <summary>
        /// 配置更改时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void _redisConfigurationChanged(object sender, EndPointEventArgs e)
        {
            Debug.WriteLine($"RedisConfigurationChanged:{e.EndPoint}");
        }

        /// <summary>
        /// redis类库错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void _redisInternalError(object sender, InternalErrorEventArgs e)
        {
            Debug.WriteLine($"RedisInternalError:Message:{e.Exception.Message}");
        }

        /// <summary>
        /// 更改集群
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void _redisHashSlotMoved(object sender, HashSlotMovedEventArgs e)
        {
            Debug.WriteLine($"RedisHashSlotMoved:NewEndPoint【{e.NewEndPoint}】OldEndPoint:【{e.OldEndPoint}】");
        }

        /// <summary>
        /// 发生错误时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void _redisErrorMessage(object sender, RedisErrorEventArgs e)
        {
            Debug.WriteLine($"RedisErrorMessage:{e.Message}");
        }

        /// <summary>
        /// 连接失败 ， 如果重新连接成功你将不会收到这个通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void _redisConnectionFailed(object sender, ConnectionFailedEventArgs e)
        {
            Debug.WriteLine($"RedisConnectionFailed:重新连接:Endpoint【{e.EndPoint}】,【{e.FailureType}】,【{(e.Exception == null ? string.Empty : e.Exception.Message)}】");
        }

        #endregion
    }
}
