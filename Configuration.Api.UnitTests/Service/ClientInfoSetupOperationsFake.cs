using Configuration.DataTransfer.ClientInfoSetup;
using Configuration.DomainModel.ClientInfoSetup;
using Configuration.DomainModel.ClientInfoSetup.Request;
using Configuration.DomainModel.ClientInfoSetup.Response;
using Configuration.RepositoryHandler.MsSql.EF.ClientInfoSetup;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Configuration.Api.UnitTests.Service
{
    public class ClientInfoSetupOperationsFake : IClientInfoSetupOperations
    {
        private readonly List<GetBESBUInfoByClientIdResponseModel> responseModels;
        public ClientInfoSetupOperationsFake()
        {
            responseModels = new List<GetBESBUInfoByClientIdResponseModel>()
            {
                new GetBESBUInfoByClientIdResponseModel(){ClientId=0,Name="Test"}
            };
        }
        public Task<Config_tblClientSBUMapDto> AddAsync(Config_tblClientSBUMapDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRangeAsync(IList<Config_tblClientSBUMapDto> entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Config_tblClientSBUMapDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRangeAsync(IList<Config_tblClientSBUMapDto> entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Config_tblClientSBUMapDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Config_tblClientSBUMapDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DataSet> GetClientList(GetBEClientInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetBEClientInfoResponse>> GetClients(GetBEClientInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetBEClientInfoResponse>> GetClientsUsingDapper(GetBEClientInfoRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<DataSet> GetDataSet(string sql, List<SqlParameter> parameters)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetMaxClientId()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetBESBUInfoByClientIdResponseModel>> GetSBUListbasedONClient(int iclientId)
        {
            return await Task.FromResult(responseModels);
        }

        public Task ManageClientData(AddClientInfoSetupRequest oClient)
        {
            throw new NotImplementedException();
        }

        public Task ManageClientData(UpdateClientInfoSetupRequest oClient)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Config_tblClientSBUMapDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRangeAsync(IList<Config_tblClientSBUMapDto> entity)
        {
            throw new NotImplementedException();
        }
    }
}
