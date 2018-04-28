using Dapper;
using Dapper.Contrib.Extensions;
using Lynx.Extension;
using MySql.Data.MySqlClient;
using SnowLeopard.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
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
        /// <returns></returns>
        public virtual long Insert(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Insert(model, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sqlAdapter"></param>
        /// <returns></returns>
        public async virtual Task<long> InsertAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null, ISqlAdapter sqlAdapter = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.InsertAsync(model, transaction, commandTimeout, sqlAdapter);
            }
        }

        /// <summary>
        /// BatchInsert
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="models"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public virtual long BatchInsert(string sql, T models = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Execute(sql, models, transaction, commandTimeout, commandType);
        }

        /// <summary>
        /// BatchInsertAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="models"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public async virtual Task<long> BatchInsertAsync(string sql, T models = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return await ExecuteAsync(sql, models, transaction, commandTimeout, commandType);
        }

        #endregion

        #region Delete

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual bool Delete(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot be null", nameof(id));

            var sql = $"DELETE FROM {typeof(T).GetTableName()} WHERE id=@id";

            using (var conn = new MySqlConnection(_connStr))
            {
                return conn.Execute(sql, new { id }, transaction, commandTimeout, CommandType.Text) > 0;
            }
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public async virtual Task<bool> DeleteAsync(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot be null", nameof(id));

            var sql = $"DELETE FROM {typeof(T).GetTableName()} WHERE id=@id";

            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.ExecuteAsync(sql, new { id }, transaction, commandTimeout, CommandType.Text) > 0;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// <returns></returns>
        public async virtual Task<bool> UpdateAsync(T model, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.UpdateAsync(model, transaction, commandTimeout);
            }
        }

        #endregion

        #region Get

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public virtual T Get(object id, IDbTransaction transaction = null, int? commandTimeout = null)
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
        /// <returns></returns>
        public async virtual Task<T> GetAsync(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.GetAsync<T>(id, transaction, commandTimeout);
            }
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
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
        /// <returns></returns>
        public async virtual Task<IEnumerable<T>> GetAllAsync(IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var conn = new MySqlConnection(_connStr))
            {
                return await conn.GetAllAsync<T>(transaction, commandTimeout);
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
        /// <returns></returns>
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
        /// <returns></returns>
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
