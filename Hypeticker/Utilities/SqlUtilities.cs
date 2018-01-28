using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Hypeticker.Utilities
{
    public static class SqlUtilities
    {
        public static async Task<T> RunSqlAsync<T>(Func<SqlConnection, Task<T>> app)
        {
            var str = ConfigurationManager.ConnectionStrings["SqlConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(str))
            {
                await conn.OpenAsync();
                return await app(conn);
            }
        }

        public static async Task<int> RunStoredProc(string spName, object parameters)
        {
            Func<SqlConnection, Task<int>> sp = async (conn) => await conn.ExecuteAsync($"exec {spName}", parameters, commandType: CommandType.StoredProcedure);
            return await RunSqlAsync<int>(sp);
        }
    }
}
