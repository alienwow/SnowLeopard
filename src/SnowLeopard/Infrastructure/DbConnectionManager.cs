using System.Data;
using System.Data.Common;

namespace SnowLeopard.Infrastructure
{
    /// <summary>
    /// DbConnectionManager
    /// </summary>
    public class DbConnectionManager
    {
        private readonly DbProviderFactory _dbProviderFactory;
        public DbConnectionManager(DbProviderFactory dbProviderFactory)
        {
            _dbProviderFactory = dbProviderFactory;
        }
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        public IDbConnection Create(string connStr)
        {
            IDbConnection dbConn = _dbProviderFactory.CreateConnection();
            dbConn.ConnectionString = connStr;
            if (dbConn.State == ConnectionState.Closed)
            {
                dbConn.Open();
            }
            return dbConn;
        }
    }
}
