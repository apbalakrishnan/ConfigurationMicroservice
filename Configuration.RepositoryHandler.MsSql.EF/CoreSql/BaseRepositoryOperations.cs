using Configuration.DataTransfer;
using Configuration.Repository;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using Microsoft.Data.SqlClient;

namespace Configuration.RepositoryHandler.MsSql.EF.CoreSql
{
    public class BaseRepositoryOperations<T> : IAsyncRepositoryOperations<T>
        where T : BaseEntity
    {
        private readonly ApplicationDbContext applicationDbContext;

        public BaseRepositoryOperations(ApplicationDbContext nerveHubDbContext)
        {
            this.applicationDbContext = nerveHubDbContext ?? throw new ArgumentNullException(nameof(nerveHubDbContext));
        }

        public ApplicationDbContext AppDbContext
        {
            get
            {
                return applicationDbContext;
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            applicationDbContext.Set<T>().Add(entity);
            await applicationDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            applicationDbContext.Set<T>().Remove(entity);
            await applicationDbContext.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await applicationDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await applicationDbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            applicationDbContext.ChangeTracker.Clear();
            applicationDbContext.Entry(entity).State = EntityState.Modified;
            await applicationDbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateRangeAsync(IList<T> entity)
        {
            applicationDbContext.UpdateRange(entity);
            return await applicationDbContext.SaveChangesAsync();
        }

        public async Task<int> AddRangeAsync(IList<T> entity)
        {
            applicationDbContext.Set<T>().AddRange(entity);
            return await applicationDbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteRangeAsync(IList<T> entity)
        {
            applicationDbContext.Set<T>().RemoveRange(entity);
            return await applicationDbContext.SaveChangesAsync();
        }
        public async Task<DataSet> GetDataSet(string sql, List<SqlParameter> parameters)
        {
            DataSet result = new();

            var connection = applicationDbContext.Database.GetDbConnection();

            using (DbCommand cmd = connection.CreateCommand())
            {
                cmd.CommandText = sql;

                if (parameters != null)
                {
                    foreach (var item in parameters)
                    {
                        cmd.Parameters.Add(item);
                    }
                }

                if (connection.State != System.Data.ConnectionState.Open)
                    // Open database connection  
                    await connection.OpenAsync();
                //AppDbContext.Database.OpenConnection();

                // Create a DataReader  
                using DbDataReader reader = await cmd.ExecuteReaderAsync();
                do
                {
                    // loads the DataTable (schema will be fetch automatically)
                    var tb = new DataTable();
                    tb.Load(reader);
                    result.Tables.Add(tb);

                } while (!reader.IsClosed);
            }
            return await Task.FromResult(result);
        }
    }
}
