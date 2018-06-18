using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace SnowLeopard.Abstractions
{
    /// <summary>
    /// IBaseDAL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IBaseDAL<T> : IDisposable
        where T : class
    {
        /// <summary>
        /// ConnStr
        /// </summary>
        string ConnStr { get; }

        /// <summary>
        /// DbConnection
        /// </summary>
        IDbConnection DbConnection { get; }

        #region Insert

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Identity of inserted entity</returns>
        long Insert(T model, int? commandTimeout = null);

        /// <summary>
        /// InsertAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="sqlAdapter"></param>
        /// <returns>Identity of inserted entity</returns>
        Task<int> InsertAsync(T model, int? commandTimeout = null, ISqlAdapter sqlAdapter = null);

        #endregion

        #region Delete

        #region Int

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        int Delete(int id, int? commandTimeout = null);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        Task<int> DeleteAsync(int id, int? commandTimeout = null);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        int Delete(IEnumerable<int> ids, int? commandTimeout = null);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        Task<int> DeleteAsync(IEnumerable<int> ids, int? commandTimeout = null);

        #endregion

        #region String

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        int Delete(string id, int? commandTimeout = null);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        Task<int> DeleteAsync(string id, int? commandTimeout = null);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        int Delete(IEnumerable<string> ids, int? commandTimeout = null);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        Task<int> DeleteAsync(IEnumerable<string> ids, int? commandTimeout = null);

        #endregion

        #region Guid

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        int Delete(Guid id, int? commandTimeout = null);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        Task<int> DeleteAsync(Guid id, int? commandTimeout = null);

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        int Delete(IEnumerable<Guid> ids, int? commandTimeout = null);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>The number of rows affected.</returns>
        Task<int> DeleteAsync(IEnumerable<Guid> ids, int? commandTimeout = null);

        #endregion

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if not found</returns>
        bool Delete(T model, int? commandTimeout = null);

        /// <summary>
        /// DeleteAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if not found</returns>
        Task<bool> DeleteAsync(T model, int? commandTimeout = null);

        /// <summary>
        /// DeleteAll
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if none found</returns>
        bool DeleteAll(int? commandTimeout = null);

        /// <summary>
        /// DeleteAllAsync
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>true if deleted, false if none found</returns>
        Task<bool> DeleteAllAsync(int? commandTimeout = null);

        #endregion

        #region Update

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        bool Update(T model, int? commandTimeout = null);

        /// <summary>
        /// UpdateAsync
        /// </summary>
        /// <param name="model"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>true if updated, false if not found or not modified (tracked entities)</returns>
        Task<bool> UpdateAsync(T model, int? commandTimeout = null);

        #endregion

        #region Get

        #region Int

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        T Get(int id, int? commandTimeout = null);

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        Task<T> GetAsync(int id, int? commandTimeout = null);

        #endregion

        #region String

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        T Get(string id, int? commandTimeout = null);

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        Task<T> GetAsync(string id, int? commandTimeout = null);

        #endregion

        #region Guid

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        T Get(Guid id, int? commandTimeout = null);

        /// <summary>
        /// GetAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        Task<T> GetAsync(Guid id, int? commandTimeout = null);

        #endregion

        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        IEnumerable<T> GetAll(int? commandTimeout = null);

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="commandTimeout"></param>
        /// <returns>Entity of T</returns>
        Task<IEnumerable<T>> GetAllAsync(int? commandTimeout = null);

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
        IEnumerable<T> Query(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

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
        Task<IEnumerable<T>> QueryAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);

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
        IEnumerable<Model> Query<Model>(string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null);

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
        Task<IEnumerable<Model>> QueryAsync<Model>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);

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
        int Execute(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="commandTimeout"></param>
        /// <param name="commandType"></param>
        /// <returns>The number of rows affected.</returns>
        Task<int> ExecuteAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);

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
        Model ExecuteScalar<Model>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// ExecuteScalarAsync
        /// </summary>
        /// <typeparam name="Model">The type to return.</typeparam>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        Task<Model> ExecuteScalarAsync<Model>(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// ExecuteScalar
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        object ExecuteScalar(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);

        /// <summary>
        /// ExecuteScalarAsync
        /// </summary>
        /// <param name="sql">The SQL to execute.</param>
        /// <param name="param">The parameters to use for this command.</param>
        /// <param name="commandTimeout">Number of seconds before command execution timeout.</param>
        /// <param name="commandType">Is it a stored proc or a batch?</param>
        /// <returns>The first cell returned, as System.Object.</returns>
        Task<object> ExecuteScalarAsync(string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null);

        #endregion


        void Close();
    }
}
