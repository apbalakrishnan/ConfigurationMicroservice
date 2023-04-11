using Configuration.Common.Utility;
using Configuration.DataTransfer.ClientInfoSetup;
using Configuration.DomainModel.ClientInfoSetup;
using Configuration.DomainModel.ClientInfoSetup.Request;
using Configuration.DomainModel.ClientInfoSetup.Response;
using Configuration.RepositoryHandler.MsSql.EF.ClientInfoSetup;
using Configuration.RepositoryHandler.MsSql.EF.CoreSql;
using System.Data;
using System.Diagnostics;

namespace Configuration.Repository.ClientInfoSetup
{
    public interface IClientInfoSetupService
    {
        Task<IEnumerable<GetBESBUInfoByClientIdResponseModel>> GetSBUListbasedONClient(int iclientId);
        Task<int> InsertData(AddClientInfoSetupRequest oClient);
        Task<int> UpdateData(UpdateClientInfoSetupRequest oClient);
        Task<List<GetBEClientInfoResponse>> GetClientList(GetBEClientInfoRequest request);
    }

    public class ClientInfoSetupService : IClientInfoSetupService
    {
        private readonly IClientInfoSetupOperations clientInfoOperations;
        public ClientInfoSetupService(IClientInfoSetupOperations clientInfoOperations)
        {
            this.clientInfoOperations = clientInfoOperations;
        }

        public async Task<IEnumerable<GetBESBUInfoByClientIdResponseModel>> GetSBUListbasedONClient(int iclientId)
        {
            // var clientList = await clientInfoOperations.GetClientList(new GetBEClientInfoRequest() { ClientName = "ABC Demo", UserID = -1 });
            return await clientInfoOperations.GetSBUListbasedONClient(iclientId);
        }

        public async Task<int> InsertData(AddClientInfoSetupRequest oClient)
        {
            List<Config_tblClientSBUMapDto> Config_tblClientSBUMaps = new();

            await clientInfoOperations.ManageClientData(oClient);

            int lastInsertedClientId = await clientInfoOperations.GetMaxClientId();

            foreach (var item in oClient.ClientInfoSetupModel.SbuClientIds)
            {
                Config_tblClientSBUMapDto config_TblClientSBUMapDto = new()
                {
                    ClientId = lastInsertedClientId,
                    SBUId = item,
                    Disabled = false,
                    CreatedBy = oClient.ClientInfoSetupModel.CreatedBy,
                    ModifiedBy = oClient.ClientInfoSetupModel.CreatedBy
                };

                Config_tblClientSBUMaps.Add(config_TblClientSBUMapDto);
                // var result = await clientInfoOperations.AddAsync(config_TblClientSBUMapDto);
            }

            await clientInfoOperations.AddRangeAsync(Config_tblClientSBUMaps);

            return lastInsertedClientId;
        }

        public async Task<int> UpdateData(UpdateClientInfoSetupRequest oClient)
        {

            await clientInfoOperations.ManageClientData(oClient);

            return 0;
        }

        public async Task<List<GetBEClientInfoResponse>> GetClientList(GetBEClientInfoRequest request)
        {
            //  var resulttest = await clientInfoOperations.GetSBUListbasedONClient(2);

            Stopwatch stopwatch = new();
            stopwatch.Start();

            var clients = await clientInfoOperations.GetClients(request); // RawSql

            stopwatch.Stop();
            var timeTaken = stopwatch.ElapsedMilliseconds; // 140
            Console.WriteLine($"RawSql : {timeTaken}");

            stopwatch.Reset();
            stopwatch.Start();

            var clientListds = await clientInfoOperations.GetClientList(request); // Dataset

            stopwatch.Stop();
            var timeTakends = stopwatch.ElapsedMilliseconds; // 100 
            Console.WriteLine($"Dataset : {timeTakends}");


            stopwatch.Reset();
            stopwatch.Start();

            var clientdapper = await clientInfoOperations.GetClientsUsingDapper(request); // 101

            stopwatch.Stop();
            var timeTakendapper = stopwatch.ElapsedMilliseconds; // 101
            Console.WriteLine($"Dapper : {timeTakendapper}");

            if (clientListds != null && clientListds.Tables.Count > 0 && clientListds.Tables[0].Rows.Count > 0)
            {
                var clientList = clientListds.Tables[0].AsEnumerable().Select(x => new GetBEClientInfoResponse()
                {
                    ClientID = Convert.ToInt16(x["ClientID"].ToString()),
                    ClientName = x["ClientName"].ToString().DecodeHtmlString() + " " + (Convert.ToBoolean(x["Disabled"]) ? "(Disabled)" : ""),
                    Disabled = Convert.ToBoolean(x["Disabled"]),
                    Description = x["Description"].ToString().DecodeHtmlString(),
                    EndDate = Convert.ToDateTime(x["EndDate"].ToString()),
                    CreatedBy = 0
                }).ToList();

                return clientList;
            }
            else
            {
                return new List<GetBEClientInfoResponse>() { };
            }
        }

    }
}
