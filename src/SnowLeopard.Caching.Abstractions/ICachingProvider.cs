using System;
using System.Threading.Tasks;

namespace SnowLeopard.Caching.Abstractions
{
    /// <summary>
    /// ICacheProvider
    /// </summary>
    public interface ICachingProvider
    {
        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// Set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        void Set<T>(string key, T value, TimeSpan timeSpan);

        /// <summary>
        /// Set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        Task SetAsync<T>(string key, T value, TimeSpan timeSpan);

        /// <summary>
        /// Lock
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        bool Lock(string key, int db = 0, TimeSpan? timeSpan = null);

        /// <summary>
        /// Lock
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        Task<bool> LockAsync(string key, int db = 0, TimeSpan? timeSpan = null);

        /// <summary>
        /// UnLock
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        bool UnLock(string key, int db = 0);

        /// <summary>
        /// UnLock
        /// </summary>
        /// <param name="key"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        Task<bool> UnLockAsync(string key, int db = 0);

    }
}
