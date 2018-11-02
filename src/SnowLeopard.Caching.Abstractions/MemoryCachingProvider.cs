using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace SnowLeopard.Caching.Abstractions
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
            return default(T);
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
    }
}
