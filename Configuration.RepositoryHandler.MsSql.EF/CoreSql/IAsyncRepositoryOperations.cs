using Configuration.DataTransfer;
using Microsoft.Data.SqlClient;
using System.Data;


namespace Configuration.Repository
{
    public interface IAsyncRepositoryOperations<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<int> UpdateRangeAsync(IList<T> entity);
        Task<int> AddRangeAsync(IList<T> entity);
        Task<int> DeleteRangeAsync(IList<T> entity);
        Task<DataSet> GetDataSet(string sql, List<SqlParameter> parameters);
    }
}
