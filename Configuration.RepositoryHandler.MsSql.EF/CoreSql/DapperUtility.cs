using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace Configuration.RepositoryHandler.MsSql.EF.CoreSql
{
    public class DapperUtility
    {
        static Lazy<DapperUtility> objDapper = null;
        private static string connStr = string.Empty;

        public static DapperUtility CreateInstance(string connString)
        {
            connStr = connString;

            objDapper = new Lazy<DapperUtility>();

            return objDapper.Value;
        }

        internal IDbConnection GetOpenConnection()
        {
            var connection = new SqlConnection(connStr);
            connection.Open();
            return connection;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string storedProcName, DynamicParameters DynamicParameters) where T : class
        {
            using (var connection = GetOpenConnection())
            {
                var result = (await connection.QueryAsync<T>(storedProcName, DynamicParameters, commandType: CommandType.StoredProcedure));
                return result.ToList();
            }
        }
        public async Task<List<T>> GetAll<T>(string queryText) where T : class
        {
            using (var connection = GetOpenConnection())
            {
                var result = (await connection.QueryAsync<T>(queryText));
                return result.ToList();
            }
        }

        public async Task<T> Get<T>(string queryText) where T : class
        {
            using (var connection = GetOpenConnection())
            {
                var result = await connection.QueryAsync<T>(queryText.ToString());
                return result.FirstOrDefault();
            }
        }

        public object ExecuteStoredProcedure(string storedProcName, DynamicParameters dynamicParameters, int queryTimeout = 0)
        {

            using (var connection = GetOpenConnection())
            {
                return queryTimeout > 0 ?
                    connection.Execute(storedProcName, param: dynamicParameters, commandTimeout: queryTimeout, commandType: CommandType.StoredProcedure) :
                    connection.Execute(storedProcName, param: dynamicParameters, commandType: CommandType.StoredProcedure);
            }
        }

        public object GetScalar(StringBuilder queryText)
        {
            using (var connection = GetOpenConnection())
            {
                return connection.ExecuteScalarAsync(queryText.ToString());
            }
        }
    }
}
