using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using SnowLeopard.Caching.Abstractions;

namespace SnowLeopard.Caching
{
    public class MemoryCachingProvider : ICachingProvider
    {
        private IMemoryCache _cache;

        public MemoryCachingProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string cacheKey)
        {
            if (_cache.Get(cacheKey) is T result)
                return result;
            return default;
        }

        public async Task<T> GetAsync<T>(string cacheKey)
        {
            return await Task.FromResult(Get<T>(cacheKey));
        }

        public void Set<T>(string cacheKey, T cacheValue, TimeSpan absoluteExpirationRelativeToNow)
        {
            _cache.Set(cacheKey, cacheValue, absoluteExpirationRelativeToNow);
        }

        public async Task SetAsync<T>(string cacheKey, T cacheValue, TimeSpan absoluteExpirationRelativeToNow)
        {
            await Task.Run(() =>
            {
                _cache.Set(cacheKey, cacheValue, absoluteExpirationRelativeToNow);
            });
        }

        public bool Lock(string key, int db = 0, TimeSpan? timeSpan = null)
        {
            throw new Exception("MemoryCachingProvider 不支持分布式锁");
        }

        public Task<bool> LockAsync(string key, int db = 0, TimeSpan? timeSpan = null)
        {
            throw new Exception("MemoryCachingProvider 不支持分布式锁");
        }

        public bool UnLock(string key, int db = 0)
        {
            throw new Exception("MemoryCachingProvider 不支持分布式锁");
        }

        public Task<bool> UnLockAsync(string key, int db = 0)
        {
            throw new Exception("MemoryCachingProvider 不支持分布式锁");
        }
    }
}
