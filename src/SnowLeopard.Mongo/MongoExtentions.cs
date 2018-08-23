using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace SnowLeopard.Mongo
{
    /// <summary>
    /// MongoExtentions
    /// </summary>
    public static class MongoExtentions
    {
        #region Insert

        public static void Insert<T>(this IMongoCollection<T> collenction, T entity)
        => collenction.InsertOne(entity);

        public async static Task InsertAsync<T>(this IMongoCollection<T> collenction, T entity)
                  => await collenction.InsertOneAsync(entity);

        public static void Insert<T>(this IMongoCollection<T> collenction, IEnumerable<T> entity)
                 => collenction.InsertMany(entity);

        public async static Task InsertAsync<T>(this IMongoCollection<T> collenction, IEnumerable<T> entity)
                 => await collenction.InsertManyAsync(entity);

        #endregion

        #region Delete

        /// <summary>
        /// 删除全部匹配数据
        /// </summary>
        public async static Task DeleteAsync<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter)
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            await collenction.DeleteManyAsync(filter);
        }

        /// <summary>
        /// 删除一个
        /// </summary>
        public async static Task DeleteOneAsync<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter)
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            await collenction.DeleteOneAsync(filter);
        }

        #endregion

        #region Query

        /// <summary>
        /// 查找第一个
        /// </summary>
        public async static Task<T> FirstOrDefaultAsync<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter, FindOptions options = null)
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            return await collenction.Find(filter, options).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查找第一个
        /// </summary>
        public static T FirstOrDefault<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter, FindOptions options = null)
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            return collenction.Find(filter, options).FirstOrDefault();
        }

        /// <summary>
        /// 查找符合数据列表
        /// </summary>
        public async static Task<List<T>> FindToListAsync<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter, FindOptions options = null)
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            return await collenction.Find(filter, options).ToListAsync();
        }

        /// <summary>
        /// 查找符合数据列表
        /// </summary>
        public static List<T> FindToList<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter, FindOptions options = null)
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            return collenction.Find(filter, options).ToList();
        }

        #endregion

        #region FindOneAndUpdate

        /// <summary>
        /// FindOneAndUpdateAsync
        /// </summary>
        public async static Task<T> FindOneAndUpdateAsync<T>(this IMongoCollection<T> collenction,
            Expression<Func<T, bool>> filter,
            UpdateDefinition<T> update,
            bool isUpsert,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            if (update == null) { throw new ArgumentNullException(nameof(update)); }

            FindOneAndUpdateOptions<T, T> options = null;
            if (isUpsert)
            {
                options = new FindOneAndUpdateOptions<T, T>
                {
                    IsUpsert = isUpsert
                };
            }
            return await collenction.FindOneAndUpdateAsync<T, T>(filter, update, options, cancellationToken);
        }

        /// <summary>
        /// FindOneAndUpdate
        /// </summary>
        public static T FindOneAndUpdate<T>(this IMongoCollection<T> collenction,
            Expression<Func<T, bool>> filter,
            UpdateDefinition<T> update,
            bool isUpsert,
            CancellationToken cancellationToken = default(CancellationToken)
        )
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            if (update == null) { throw new ArgumentNullException(nameof(update)); }

            FindOneAndUpdateOptions<T, T> options = null;
            if (isUpsert)
            {
                options = new FindOneAndUpdateOptions<T, T>
                {
                    IsUpsert = isUpsert
                };
            }
            return collenction.FindOneAndUpdate<T, T>(filter, update, options, cancellationToken);
        }

        #endregion
    }
}
