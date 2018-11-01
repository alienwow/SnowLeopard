using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SnowLeopard.Redis;

namespace SnowLeopard.Caching.Redis
{
    /// <summary>
    /// RedisCacheProvider
    /// </summary>
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly ILogger _logger;
        private readonly IRedisCache _cache;

        public RedisCacheProvider(
            ILogger<RedisCacheProvider> logger,
            IRedisCache cache
        )
        {
            _logger = logger;
            _cache = cache;
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return _cache.Get<T>(key);
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            return await _cache.GetAsync<T>(key);
        }

        /// <summary>
        /// Set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        public void Set<T>(string key, T value, TimeSpan timeSpan)
        {
            _cache.Set(key, value, timeSpan: timeSpan);
        }

        /// <summary>
        /// Set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public async Task SetAsync<T>(string key, T value, TimeSpan timeSpan)
        {
            await _cache.SetAsync(key, value, timeSpan: timeSpan);
        }
    }
}
