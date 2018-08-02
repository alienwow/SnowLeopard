using System;
using System.Collections.Generic;

namespace SnowLeopard.Model.BaseModels
{
    /// <summary>
    /// PagingDTO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingDTO<T>
    {
        /// <summary>
        /// PagingDTO
        /// </summary>
        public PagingDTO() { }

        /// <summary>
        /// PagingDTO
        /// </summary>
        /// <param name="list"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="count"></param>
        public PagingDTO(IEnumerable<T> list, int page, int pageSize, long count)
        {
            List = list;
            Page = page;
            PageSize = pageSize;
            Count = count;
        }

        /// <summary>
        /// 分页数据
        /// </summary>
        public IEnumerable<T> List { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public long Count { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// 分页数
        /// </summary>
        public int PageCount => Count <= 0 ? 0 : (int)Math.Ceiling(1.0 * Count / PageSize);

        /// <summary>
        /// 分页大小
        /// </summary>
        public int PageSize { get; set; }
    }
}
