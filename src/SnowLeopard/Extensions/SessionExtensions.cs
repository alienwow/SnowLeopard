using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SnowLeopard.Infrastructure.Json;

namespace SnowLeopard.Extensions
{
    /// <summary>
    /// SessionExtensions
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Set
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetAsync(key, value).Wait();
        }

        /// <summary>
        /// SetAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static async Task SetAsync<T>(this ISession session, string key, T value)
        {
            session.SetString(key, SnowLeopardJsonConvert.SerializeObject(value));
            await session.CommitAsync();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default :
                                  SnowLeopardJsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="session"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static async Task<T> GetAsync<T>(this ISession session, string key)
        {
            await session.LoadAsync();
            var value = session.GetString(key);
            return value == null ? default :
                                  SnowLeopardJsonConvert.DeserializeObject<T>(value);
        }
    }
}
