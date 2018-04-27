using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// BaseApiController
    /// </summary>
    public abstract class BaseApiController : Controller
    {
        /// <summary>
        /// BaseResult
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">data</param>
        /// <param name="statusCode">状态码</param>
        /// <param name="msg">msg</param>
        /// <returns></returns>
        public virtual BaseViewModel<T> BaseResult<T>(T data, int statusCode = 200, string msg = "success")
        {
            return new BaseViewModel<T>()
            {
                Code = statusCode,
                Msg = msg,
                Data = data
            };
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">分页数据</param>
        /// <param name="page">页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="count">总条数</param>
        /// <param name="statusCode">状态码</param>
        /// <param name="msg">msg</param>
        /// <returns></returns>
        public virtual BaseViewModel<PagingViewModel<T>> BaseResult<T>(IEnumerable<T> list, int page, int pageSize, long count, int statusCode = 200, string msg = "success")
        {
            return new BaseViewModel<PagingViewModel<T>>()
            {
                Code = statusCode,
                Msg = msg,
                Data = new PagingViewModel<T>(list, page, pageSize, count)
            };
        }
    }

}
