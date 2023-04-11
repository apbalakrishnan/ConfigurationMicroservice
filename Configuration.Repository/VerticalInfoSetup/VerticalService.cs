using Configuration.DataTransfer.VerticalInfoSetup;
using Configuration.RepositoryHandler.MsSql.EF.VerticalInfoSetup;

namespace Configuration.Repository.VerticalInfoSetup
{
    public interface IVerticalService
    {
        Task<IEnumerable<Config_tblVerticalMaster>> GetVerticalList(bool isActiveVertical);
    }

    public class VerticalService : IVerticalService
    {
        private readonly IVerticalInfoOperations verticalInfoOperations;

        public VerticalService(IVerticalInfoOperations verticalInfoOperations)
        {
            this.verticalInfoOperations = verticalInfoOperations;
        }
        public async Task<IEnumerable<Config_tblVerticalMaster>> GetVerticalList(bool isActiveVertical)
        {
            return await verticalInfoOperations.GetVerticalList(isActiveVertical);
        }
    }
}
