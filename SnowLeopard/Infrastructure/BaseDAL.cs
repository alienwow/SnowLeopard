using Dapper;
using Dapper.Contrib.Extensions;
using Lynx.Extension;
using MySql.Data.MySqlClient;
using SnowLeopard.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// BaseDAL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseDAL<T> : IBaseDAL<T>
        where T : class
    {
        private string _connStr = string.Empty;

        /// <summary>
        /// ConnStr
        /// </summary>
        public string ConnStr => _connStr;

        /// <summary>
        /// BaseDAL
        /// </summary>
        /// <param name="connStr"></param>
        public BaseDAL(string connStr) { _connStr = connStr; }

        #region Insert

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Identity of inserted entity</returns>
        public virtual int Insert(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return (int)conn.Insert(model, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sqlAdapter"></param>
        /// <returns>Identity of inserted entity</returns>
        public async virtual Task<int> InsertAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.InsertAsync(model, transaction, commandTimeout, sqlAdapter);
            }
        }

        #endregion

        #region Delete

        private const string _deleteObjSql = "DELETE FROM {0} WHERE id=@id";
        private const string _deleteObjsSql = "DELETE FROM {0} WHERE id IN @ids";

        #region Int

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(int id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id) + "必须大于0");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Execute(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            }
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(int id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id) + "必须大于0");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.ExecuteAsync(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<int> ids, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Execute(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            }
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<int> ids, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.ExecuteAsync(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            }
        }

        #endregion

        #region String

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(string id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot be null", nameof(id));

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Execute(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            }
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(string id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot be null", nameof(id));

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.ExecuteAsync(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<string> ids, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Execute(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            }
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<string> ids, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.ExecuteAsync(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            }
        }

        #endregion

        #region Guid

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(Guid id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id) + " 不是标准的Guid");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Execute(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            }
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(Guid id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id) + " 不是标准的Guid");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.ExecuteAsync(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<Guid> ids, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Execute(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            }
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<Guid> ids, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.ExecuteAsync(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            }
        }

        #endregion

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if not found</returns>
        public virtual bool Delete(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Delete(model, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if not found</returns>
        public async virtual Task<bool> DeleteAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.DeleteAsync(model, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// DeleteAll
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if none found</returns>
        public virtual bool DeleteAll(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.DeleteAll<T>(transaction, commandTimeout);
            }
        }

        /// <summary>
        /// DeleteAllAsync
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if none found</returns>
        public async virtual Task<bool> DeleteAllAsync(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.DeleteAllAsync<T>(transaction, commandTimeout);
            }
        }

        #endregion

        #region Update

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public virtual bool Update(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Update(model, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public async virtual Task<bool> UpdateAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.UpdateAsync(model, transaction, commandTimeout);
            }
        }

        #endregion

        #region Get

        #region Int

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual T Get(int id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Get<T>(id, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(int id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }

        #endregion

        #region String

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual T Get(string id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Get<T>(id, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(string id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }

        #endregion

        #region Guid

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual T Get(Guid id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Get<T>(id, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(Guid id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }

        #endregion

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual IEnumerable<T> GetAll(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.GetAll<T>(transaction, commandTimeout);
            }
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<IEnumerable<T>> GetAllAsync(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.GetAllAsync<T>(transaction, commandTimeout);
            }
        }

        #endregion

        #region Query

        /// <summary>
        /// Query
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public virtual IEnumerable<T> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public async virtual Task<IEnumerable<T>> QueryAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public virtual IEnumerable<Model> Query<Model>(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Query<Model>(sql, param, transaction, buffered, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public async virtual Task<IEnumerable<Model>> QueryAsync<Model>(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.QueryAsync<Model>(sql, param, transaction, commandTimeout, commandType);
            }
        }

        #endregion

        #region Execute

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Execute(sql, param, transaction, commandTimeout, commandType);
            }
        }

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> ExecuteAsync(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
            }
        }

        #endregion

    }
}
