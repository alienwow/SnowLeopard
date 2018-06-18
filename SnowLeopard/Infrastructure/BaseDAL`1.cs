using Dapper;
using Dapper.Contrib.Extensions;
using Lynx.Extension;
using SnowLeopard.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SnowLeopard.Infrastructure
{
    public abstract partial class BaseDAL<T> : IBaseDAL<T>
        where T : class
    {
        #region Insert

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Identity of inserted entity</returns>
        public virtual long Insert(T model, IDbTransaction transaction, int? commandTimeout = null)
        {
            long result = DbConnection.Insert(model, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sqlAdapter"></param>
        /// <returns>Identity of inserted entity</returns>
        public async virtual Task<int> InsertAsync(T model, IDbTransaction transaction, int? commandTimeout = null, ISqlAdapter sqlAdapter = null)
        {
            int result = await DbConnection.InsertAsync(model, transaction, commandTimeout, sqlAdapter);
            return result;
        }

        #endregion

        #region Delete

        #region Int

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(int id, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id) + "必须大于0");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(int id, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id) + "必须大于0");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<int> ids, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<int> ids, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            return result;
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
        public virtual int Delete(string id, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot be null", nameof(id));

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(string id, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot be null", nameof(id));

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<string> ids, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<string> ids, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            return result;
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
        public virtual int Delete(Guid id, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id) + " 不是标准的Guid");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(Guid id, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id) + " 不是标准的Guid");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { id }, transaction, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<Guid> ids, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<Guid> ids, IDbTransaction transaction, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { ids }, transaction, commandTimeout, CommandType.Text);
            return result;
        }

        #endregion

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if not found</returns>
        public virtual bool Delete(T model, IDbTransaction transaction, int? commandTimeout = null)
        {
            bool result = DbConnection.Delete(model, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if not found</returns>
        public async virtual Task<bool> DeleteAsync(T model, IDbTransaction transaction, int? commandTimeout = null)
        {
            bool result = await DbConnection.DeleteAsync(model, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// DeleteAll
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if none found</returns>
        public virtual bool DeleteAll(IDbTransaction transaction, int? commandTimeout = null)
        {
            bool result = DbConnection.DeleteAll<T>(transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// DeleteAllAsync
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if none found</returns>
        public async virtual Task<bool> DeleteAllAsync(IDbTransaction transaction, int? commandTimeout = null)
        {
            bool result = await DbConnection.DeleteAllAsync<T>(transaction, commandTimeout);
            return result;
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
        public virtual bool Update(T model, IDbTransaction transaction, int? commandTimeout = null)
        {
            bool result = DbConnection.Update(model, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public async virtual Task<bool> UpdateAsync(T model, IDbTransaction transaction, int? commandTimeout = null)
        {
            bool result = await DbConnection.UpdateAsync(model, transaction, commandTimeout);
            return result;
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
        public virtual T Get(int id, IDbTransaction transaction, int? commandTimeout = null)
        {
            T result = DbConnection.Get<T>(id, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(int id, IDbTransaction transaction, int? commandTimeout = null)
        {
            T result = await DbConnection.GetAsync<T>(id, transaction, commandTimeout);
            return result;
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
        public virtual T Get(string id, IDbTransaction transaction, int? commandTimeout = null)
        {
            T result = DbConnection.Get<T>(id, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(string id, IDbTransaction transaction, int? commandTimeout = null)
        {
            T result = await DbConnection.GetAsync<T>(id, transaction, commandTimeout);
            return result;
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
        public virtual T Get(Guid id, IDbTransaction transaction, int? commandTimeout = null)
        {
            T result = DbConnection.Get<T>(id, transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(Guid id, IDbTransaction transaction, int? commandTimeout = null)
        {
            T result = await DbConnection.GetAsync<T>(id, transaction, commandTimeout);
            return result;
        }

        #endregion

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual IEnumerable<T> GetAll(IDbTransaction transaction, int? commandTimeout = null)
        {
            IEnumerable<T> result = DbConnection.GetAll<T>(transaction, commandTimeout);
            return result;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<IEnumerable<T>> GetAllAsync(IDbTransaction transaction, int? commandTimeout = null)
        {
            IEnumerable<T> result = await DbConnection.GetAllAsync<T>(transaction, commandTimeout);
            return result;
        }

        #endregion

        #region Query

        /// <summary>
        /// Query
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public virtual IEnumerable<T> Query(string sql, IDbTransaction transaction, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<T> result = DbConnection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public async virtual Task<IEnumerable<T>> QueryAsync(string sql, IDbTransaction transaction, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<T> result = await DbConnection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <param name="param"></param>
        /// <param name="buffered"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public virtual IEnumerable<Model> Query<Model>(string sql, IDbTransaction transaction, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<Model> result = DbConnection.Query<Model>(sql, param, transaction, buffered, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public async virtual Task<IEnumerable<Model>> QueryAsync<Model>(string sql, IDbTransaction transaction, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<Model> result = await DbConnection.QueryAsync<Model>(sql, param, transaction, commandTimeout, commandType);
            return result;
        }

        #endregion

        #region Execute

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Execute(string sql, IDbTransaction transaction, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int result = DbConnection.Execute(sql, param, transaction, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="transaction"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> ExecuteAsync(string sql, IDbTransaction transaction, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int result = await DbConnection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
            return result;
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <typeparam name="Model">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="transaction">The transaction to use for this command.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public virtual Model ExecuteScalar<Model>(string sql, IDbTransaction transaction, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Model result = DbConnection.ExecuteScalar<Model>(sql, param, transaction, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// ExecuteScalarAsync
        /// </summary>
        /// <typeparam name="Model">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="transaction">The transaction to use for this command.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public async virtual Task<Model> ExecuteScalarAsync<Model>(string sql, IDbTransaction transaction, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Model result = await DbConnection.ExecuteScalarAsync<Model>(sql, param, transaction, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="transaction">The transaction to use for this command.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public virtual object ExecuteScalar(string sql, IDbTransaction transaction, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            object result = DbConnection.ExecuteScalar(sql, param, transaction, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// ExecuteScalarAsync
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="transaction">The transaction to use for this command.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public async virtual Task<object> ExecuteScalarAsync(string sql, IDbTransaction transaction, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            object result = await DbConnection.ExecuteScalarAsync(sql, param, transaction, commandTimeout, commandType);
            return result;
        }

        #endregion

    }
}
