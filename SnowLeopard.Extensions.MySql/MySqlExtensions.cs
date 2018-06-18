using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace SnowLeopard.Extensions
{
    public static class MySqlExtensions
    {
        public static void AddMySqlDbConnection(this IServiceCollection services, Func<IServiceProvider, MySqlConnection> implementationFactory)
        {
            services.AddDbConnection(implementationFactory);
        }

        public static void AddMySqlDbConnection(this IServiceCollection services, string connStr)
        {
            services.AddTransient((x) =>
            {
                IDbConnection conn = new MySqlConnection(connStr);
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                return conn;
            });
        }
    }
}
