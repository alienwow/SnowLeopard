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
    /// <summary>
    /// BaseDAL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract partial class BaseDAL<T> : IBaseDAL<T>
        where T : class
    {
        /// <summary>
        /// ConnStr
        /// </summary>
        public string ConnStr { get; private set; }

        /// <summary>
        /// DbConnection
        /// </summary>
        public IDbConnection DbConnection { get; private set; }

        /// <summary>
        /// BaseDAL
        /// </summary>
        /// <param name="dbConnection"></param>
        public BaseDAL(IDbConnection dbConnection)
        {
            DbConnection = dbConnection;
        }

        #region Insert

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Identity of inserted entity</returns>
        public virtual long Insert(T model, int? commandTimeout = null)
        {
            long result = DbConnection.Insert(model, null, commandTimeout);
            return result;
        }

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sqlAdapter"></param>
        /// <returns>Identity of inserted entity</returns>
        public async virtual Task<int> InsertAsync(T model, int? commandTimeout = null, ISqlAdapter sqlAdapter = null)
        {
            int result = await DbConnection.InsertAsync(model, null, commandTimeout, sqlAdapter);
            return result;
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
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(int id, int? commandTimeout = null)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id) + "必须大于0");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { id }, null, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(int id, int? commandTimeout = null)
        {
            if (id <= 0)
                throw new ArgumentException(nameof(id) + "必须大于0");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { id }, null, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<int> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { ids }, null, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<int> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { ids }, null, commandTimeout, CommandType.Text);
            return result;
        }

        #endregion

        #region String

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(string id, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot be null", nameof(id));

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { id }, null, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(string id, int? commandTimeout = null)
        {
            if (id == null)
                throw new ArgumentException("Cannot be null", nameof(id));

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { id }, null, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<string> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { ids }, null, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<string> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { ids }, null, commandTimeout, CommandType.Text);
            return result;
        }

        #endregion

        #region Guid

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(Guid id, int? commandTimeout = null)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id) + " 不是标准的Guid");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { id }, null, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(Guid id, int? commandTimeout = null)
        {
            if (id == Guid.Empty)
                throw new ArgumentException(nameof(id) + " 不是标准的Guid");

            var sql = string.Format(_deleteObjSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { id }, null, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Delete(IEnumerable<Guid> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = DbConnection.Execute(sql, new { ids }, null, commandTimeout, CommandType.Text);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> DeleteAsync(IEnumerable<Guid> ids, int? commandTimeout = null)
        {
            if (ids == null || ids.Count() == 0)
                throw new ArgumentException("不允许为null，且至少包含于个元素", nameof(ids));

            var sql = string.Format(_deleteObjsSql, typeof(T).GetTableName());

            int result = await DbConnection.ExecuteAsync(sql, new { ids }, null, commandTimeout, CommandType.Text);
            return result;
        }

        #endregion

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if not found</returns>
        public virtual bool Delete(T model, int? commandTimeout = null)
        {
            bool result = DbConnection.Delete(model, null, commandTimeout);
            return result;
        }

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if not found</returns>
        public async virtual Task<bool> DeleteAsync(T model, int? commandTimeout = null)
        {
            bool result = await DbConnection.DeleteAsync(model, null, commandTimeout);
            return result;
        }

        /// <summary>
        /// DeleteAll
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if none found</returns>
        public virtual bool DeleteAll(int? commandTimeout = null)
        {
            bool result = DbConnection.DeleteAll<T>(null, commandTimeout);
            return result;
        }

        /// <summary>
        /// DeleteAllAsync
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if none found</returns>
        public async virtual Task<bool> DeleteAllAsync(int? commandTimeout = null)
        {
            bool result = await DbConnection.DeleteAllAsync<T>(null, commandTimeout);
            return result;
        }

        #endregion

        #region Update

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public virtual bool Update(T model, int? commandTimeout = null)
        {
            bool result = DbConnection.Update(model, null, commandTimeout);
            return result;
        }

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        public async virtual Task<bool> UpdateAsync(T model, int? commandTimeout = null)
        {
            bool result = await DbConnection.UpdateAsync(model, null, commandTimeout);
            return result;
        }

        #endregion

        #region Get

        #region Int

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual T Get(int id, int? commandTimeout = null)
        {
            T result = DbConnection.Get<T>(id, null, commandTimeout);
            return result;
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(int id, int? commandTimeout = null)
        {
            T result = await DbConnection.GetAsync<T>(id, null, commandTimeout);
            return result;
        }

        #endregion

        #region String

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual T Get(string id, int? commandTimeout = null)
        {
            T result = DbConnection.Get<T>(id, null, commandTimeout);
            return result;
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(string id, int? commandTimeout = null)
        {
            T result = await DbConnection.GetAsync<T>(id, null, commandTimeout);
            return result;
        }

        #endregion

        #region Guid

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual T Get(Guid id, int? commandTimeout = null)
        {
            T result = DbConnection.Get<T>(id, null, commandTimeout);
            return result;
        }

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<T> GetAsync(Guid id, int? commandTimeout = null)
        {
            T result = await DbConnection.GetAsync<T>(id, null, commandTimeout);
            return result;
        }

        #endregion

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public virtual IEnumerable<T> GetAll(int? commandTimeout = null)
        {
            IEnumerable<T> result = DbConnection.GetAll<T>(null, commandTimeout);
            return result;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        public async virtual Task<IEnumerable<T>> GetAllAsync(int? commandTimeout = null)
        {
            IEnumerable<T> result = await DbConnection.GetAllAsync<T>(null, commandTimeout);
            return result;
        }

        #endregion

        #region Query

        /// <summary>
        /// Query
        /// </summary>
        /// <param name="sql"></param>
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
        public virtual IEnumerable<T> Query(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<T> result = DbConnection.Query<T>(sql, param, null, buffered, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public async virtual Task<IEnumerable<T>> QueryAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<T> result = await DbConnection.QueryAsync<T>(sql, param, null, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// Query
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="sql"></param>
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
        public virtual IEnumerable<Model> Query<Model>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<Model> result = DbConnection.Query<Model>(sql, param, null, buffered, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>
        /// A sequence of data of the supplied type; if a basic type (int, string, etc) is
        /// queried then the data from the first column in assumed, otherwise an instance
        /// is created per row, and a direct column-name===member-name mapping is assumed
        /// (case insensitive).
        /// </returns>
        public async virtual Task<IEnumerable<Model>> QueryAsync<Model>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            IEnumerable<Model> result = await DbConnection.QueryAsync<Model>(sql, param, null, commandTimeout, commandType);
            return result;
        }

        #endregion

        #region Execute

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        public virtual int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int result = DbConnection.Execute(sql, param, null, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        public async virtual Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            int result = await DbConnection.ExecuteAsync(sql, param, null, commandTimeout, commandType);
            return result;
        }

        #endregion

        #region ExecuteScalar

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <typeparam name="Model">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public virtual Model ExecuteScalar<Model>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Model result = DbConnection.ExecuteScalar<Model>(sql, param, null, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// ExecuteScalarAsync
        /// </summary>
        /// <typeparam name="Model">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public async virtual Task<Model> ExecuteScalarAsync<Model>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            Model result = await DbConnection.ExecuteScalarAsync<Model>(sql, param, null, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public virtual object ExecuteScalar(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            object result = DbConnection.ExecuteScalar(sql, param, null, commandTimeout, commandType);
            return result;
        }

        /// <summary>
        /// ExecuteScalarAsync
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        public async virtual Task<object> ExecuteScalarAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            object result = await DbConnection.ExecuteScalarAsync(sql, param, null, commandTimeout, commandType);
            return result;
        }

        #endregion

        public void Close()
        {
            if (DbConnection.State != ConnectionState.Closed)
            {
                DbConnection.Close();
            }
        }

        public void Dispose()
        {
            Close();
            DbConnection.Dispose();
        }

    }
}
