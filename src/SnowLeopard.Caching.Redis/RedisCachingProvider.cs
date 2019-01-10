using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using SnowLeopard.Caching.Abstractions;
using SnowLeopard.Redis;

namespace SnowLeopard.Caching
{
    /// <summary>
    /// RedisCachingProvider
    /// </summary>
    public class RedisCachingProvider : ICachingProvider
    {
        private readonly ILogger _logger;
        private readonly IRedisCache _cache;

        public RedisCachingProvider(
            ILogger<RedisCachingProvider> logger,
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

        /// <summary>
        /// Lock
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public bool Lock(string key, int db = 0, TimeSpan? timeSpan = null)
        {
            return _cache.Lock(key, db, timeSpan);
        }

        /// <summary>
        /// Lock
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public async Task<bool> LockAsync(string key, int db = 0, TimeSpan? timeSpan = null)
        {
            return await _cache.LockAsync(key, db, timeSpan);
        }

        /// <summary>
        /// UnLock
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public bool UnLock(string key, int db = 0)
        {
            return _cache.UnLock(key, db);
        }

        /// <summary>
        /// UnLock
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<bool> UnLockAsync(string key, int db = 0)
        {
            return await _cache.UnLockAsync(key, db);
        }
    }
}
