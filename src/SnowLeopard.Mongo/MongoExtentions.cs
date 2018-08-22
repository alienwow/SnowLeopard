using MongoDB.Driver;
using SnowLeopard.Mongo.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SnowLeopard.Mongo
{
    /// <summary>
    /// MongoExtentions
    /// </summary>
    public static class MongoExtentions
    {
        #region Insert

        public static void Insert<T>(this IMongoCollection<T> collenction, T Model) where T : TopBaseMongoEntity
                  => collenction.InsertOne(Model);

        public async static Task InsertAsync<T>(this IMongoCollection<T> collenction, T Model) where T : TopBaseMongoEntity
                  => await collenction.InsertOneAsync(Model);

        public static void Insert<T>(this IMongoCollection<T> collenction, IEnumerable<T> Model) where T : TopBaseMongoEntity
                 => collenction.InsertMany(Model);

        public async static Task InsertAsync<T>(this IMongoCollection<T> collenction, IEnumerable<T> Model) where T : TopBaseMongoEntity
                 => await collenction.InsertManyAsync(Model);

        #endregion

        #region Delete

        /// <summary>
        /// 删除全部匹配数据
        /// </summary>
        public async static Task DeleteAsync<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter) where T : TopBaseMongoEntity
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            await collenction.DeleteManyAsync(filter);
        }

        /// <summary>
        /// 删除一个
        /// </summary>
        public async static Task DeleteOneAsync<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter) where T : TopBaseMongoEntity
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            await collenction.DeleteOneAsync(filter);
        }

        #endregion

        #region Query

        /// <summary>
        /// 查找第一个
        /// </summary>
        public async static Task<T> FirstOrDefaultAsync<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter, FindOptions options = null) where T : TopBaseMongoEntity
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            return await collenction.Find(filter, options).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 查找第一个
        /// </summary>
        public static T FirstOrDefault<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter, FindOptions options = null) where T : TopBaseMongoEntity
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            return collenction.Find(filter, options).FirstOrDefault();
        }

        /// <summary>
        /// 查找符合数据列表
        /// </summary>
        public async static Task<List<T>> FindToListAsync<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter, FindOptions options = null) where T : TopBaseMongoEntity
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            return await collenction.Find(filter, options).ToListAsync();
        }

        /// <summary>
        /// 查找符合数据列表
        /// </summary>
        public static List<T> FindToList<T>(this IMongoCollection<T> collenction, Expression<Func<T, bool>> filter, FindOptions options = null) where T : TopBaseMongoEntity
        {
            if (filter == null) { throw new ArgumentNullException(nameof(filter)); }
            return collenction.Find(filter, options).ToList();
        }

        #endregion

    }
}
