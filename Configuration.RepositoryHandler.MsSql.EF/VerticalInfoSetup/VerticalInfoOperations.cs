using Configuration.DataTransfer.VerticalInfoSetup;
using Configuration.Repository;
using Configuration.RepositoryHandler.MsSql.EF.CoreSql;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Configuration.RepositoryHandler.MsSql.EF.VerticalInfoSetup
{
    public interface IVerticalInfoOperations : IAsyncRepositoryOperations<Config_tblVerticalMaster>
    {
        Task<IEnumerable<Config_tblVerticalMaster>> GetVerticalList(bool isActiveVertical);
    }

    public class VerticalInfoOperations : BaseRepositoryOperations<Config_tblVerticalMaster>, IVerticalInfoOperations
    {
        public VerticalInfoOperations(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
        public async Task<IEnumerable<Config_tblVerticalMaster>> GetVerticalList(bool isActiveVertical)
        {
            var result = await AppDbContext.ConfigtblVerticalMaster.Where(b => b.Disabled == isActiveVertical).ToListAsync();
            return result;
        }
    }
}
