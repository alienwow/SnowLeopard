using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Newtonsoft.Json;
using SnowLeopard.Caching.Abstractions;

namespace SnowLeopard.Caching
{
    /// <summary>
    /// CachingInterceptor
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CachingInterceptorAttribute : AbstractInterceptorAttribute
    {
        public CachingInterceptorAttribute()
        {
        }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var cachingAttribute = GetCachingAttributeInfo(context.ImplementationMethod);
            if (cachingAttribute != null)
            {
                await DoCaching(context, next, cachingAttribute);
            }
            else
            {
                await next(context);
            }
        }

        /// <summary>
        /// GetCachingAttributeInfo
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        private CachingAttribute GetCachingAttributeInfo(MethodInfo method)
        {
            return method.GetCustomAttributes(true)
                            .FirstOrDefault(x => x.GetType() == typeof(CachingAttribute)) as CachingAttribute;
        }

        private async Task DoCaching(
            AspectContext context,
            AspectDelegate next,
            CachingAttribute cachingAttribute
        )
        {
            var cacheProvider = context.ServiceProvider.GetService(typeof(ICachingProvider)) as ICachingProvider;
            var cachingKey = GenerateCachingKey(context, cachingAttribute);

            getCache:
            object cacheValue = await cacheProvider.GetAsync<object>(cachingKey);

            if (cacheValue != null)// 若读取到缓存就直接返回，否则设置缓存
            {
                if (context.IsAsync())
                {
                    PropertyInfo propertyInfo = context.ServiceMethod.ReturnType.GetMember("Result")[0] as PropertyInfo;
                    dynamic returnValue = JsonConvert.DeserializeObject(cacheValue.ToString(), propertyInfo.PropertyType);

                    context.ReturnValue = Task.FromResult(returnValue);
                }
                else
                {
                    context.ReturnValue = JsonConvert.DeserializeObject(cacheValue.ToString(), context.ServiceMethod.ReturnType);
                }
            }
            else
            {
                // 加分布式锁
                if (await cacheProvider.LockAsync(cachingKey, timeSpan: TimeSpan.FromSeconds(3 * 1000)))
                {
                    await next(context);

                    // 设置缓存
                    if (!string.IsNullOrWhiteSpace(cachingKey))
                    {
                        object returnValue = null;

                        if (context.IsAsync())
                            returnValue = await context.UnwrapAsyncReturnValue();
                        else
                            returnValue = context.ReturnValue;

                        await cacheProvider.SetAsync(
                                            cachingKey,
                                            returnValue,
                                            TimeSpan.FromSeconds(cachingAttribute.Expiration)
                                        );
                    }

                    // 解分布式锁
                    await cacheProvider.UnLockAsync(cachingKey);
                }
                else
                {
                    Thread.Sleep(50);
                    goto getCache;
                }
            }

        }

        /// <summary>
        /// 生成 CachingKey
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cachingAttribute"></param>
        /// <returns></returns>
        private string GenerateCachingKey(AspectContext context, CachingAttribute cachingAttribute)
        {
            var cachingKey = string.Empty;

            // 获取设置的缓存key
            if (cachingAttribute != null)
                cachingKey = cachingAttribute.Key;

            // 若未设置，则自动获取缓存key
            if (string.IsNullOrWhiteSpace(cachingKey))
            {
                string typeName = context.ServiceMethod.DeclaringType.Name;
                string methodName = context.ServiceMethod.Name;
                cachingKey = $"{typeName}{DefaultCachingKeyGenerater.SPLIT_CHAR}{methodName}";
            }

            var cachingKeyGenerater = context.ServiceProvider.GetService(typeof(ICachingKeyGenerater)) as ICachingKeyGenerater;
            IList<string> methodArguments = cachingKeyGenerater.FormatArguments(context.Parameters);

            cachingKey = cachingKeyGenerater.GenerateCachingKey(cachingKey, methodArguments);

            return cachingKey;
        }

    }
}
