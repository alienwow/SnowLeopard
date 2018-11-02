using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using Newtonsoft.Json;

namespace SnowLeopard.Caching.Abstractions
{
    /// <summary>
    /// CachingInterceptor
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CachingInterceptorAttribute : AbstractInterceptorAttribute
    {
        /// <summary>
        /// CachingKey 分割符
        /// </summary>
        public const string SPLIT_CHAR = ":";

        public CachingInterceptorAttribute()
        {
        }

        public async override Task Invoke(AspectContext context, AspectDelegate next)
        {
            var cachingAttribute = GetCachingAttributeInfo(context.ServiceMethod);
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
                IList<string> methodArguments = FormatArguments(context.ServiceMethod.GetParameters());

                cachingKey = GenerateCachingKey(typeName, methodName, methodArguments);
            }

            return cachingKey;
        }

        /// <summary>
        /// 生成 CachingKey
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="methodName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private string GenerateCachingKey(string typeName, string methodName, IList<string> parameters)
        {
            parameters.Insert(0, typeName);
            parameters.Insert(1, methodName);

            return string.Join(SPLIT_CHAR, parameters);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arguments"></param>
        /// <param name="maxCount"></param>
        /// <returns></returns>
        private IList<string> FormatArguments(ParameterInfo[] arguments, int maxCount = 5)
        {
            //arguments
            //           .Select(x =>
            //           {
            //               x.obj

            //               if (x is int || x is long || x is float || x is double || x is decimal || x is string)
            //                   return x.ToString();

            //               if (x is DateTime || x is DateTime?)
            //                   return ((DateTime)x).ToString("yyyyMMddHHmmss");

            //               if (x is IQCachable)
            //                   return ((IQCachable)x).CacheKey;
            //           })
            //           .Take(maxCount)
            //           .ToList();
            return arguments.Select(GetArgumentValue).Take(maxCount).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private string GetArgumentValue(object arg)
        {
            if (arg is int || arg is long || arg is float || arg is double || arg is decimal || arg is string)
                return arg.ToString();

            if (arg is DateTime || arg is DateTime?)
                return ((DateTime)arg).ToString("yyyyMMddHHmmss");

            //if (arg is IQCachable)
            //    return ((IQCachable)arg).CacheKey;

            return null;
        }

    }
}
