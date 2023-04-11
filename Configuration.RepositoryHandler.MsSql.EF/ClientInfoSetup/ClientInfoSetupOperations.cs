using Configuration.DataTransfer;
using Configuration.DataTransfer.ClientInfoSetup;
using Configuration.DomainModel.ClientInfoSetup;
using Configuration.DomainModel.ClientInfoSetup.Request;
using Configuration.DomainModel.ClientInfoSetup.Response;
using Configuration.Repository;
using Configuration.RepositoryHandler.MsSql.EF.CoreSql;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Configuration.RepositoryHandler.MsSql.EF.ClientInfoSetup
{
    public interface IClientInfoSetupOperations : IAsyncRepositoryOperations<Config_tblClientSBUMapDto>
    {
        Task<IEnumerable<GetBESBUInfoByClientIdResponseModel>> GetSBUListbasedONClient(int iclientId);
        Task ManageClientData(AddClientInfoSetupRequest oClient);
        Task ManageClientData(UpdateClientInfoSetupRequest oClient);
        Task<int> GetMaxClientId();
        Task<DataSet> GetClientList(GetBEClientInfoRequest request);
        Task<List<GetBEClientInfoResponse>> GetClients(GetBEClientInfoRequest request);
        Task<List<GetBEClientInfoResponse>> GetClientsUsingDapper(GetBEClientInfoRequest request);
    }

    public class ClientInfoSetupOperations : BaseRepositoryOperations<Config_tblClientSBUMapDto>, IClientInfoSetupOperations
    {
        private readonly DapperUtility dapperUtilityObj;
        public ClientInfoSetupOperations(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            dapperUtilityObj = DapperUtility.CreateInstance(applicationDbContext.Database.GetDbConnection().ConnectionString);
        }
        public async Task<IEnumerable<GetBESBUInfoByClientIdResponseModel>> GetSBUListbasedONClient(int iclientId)
        {
            var result = await (from sb in AppDbContext.Config_tblSBUMaster
                                join cl in AppDbContext.Config_tblClientSBUMap
                                on sb.SBUID equals cl.SBUId
                                where cl.ClientId == iclientId && cl.Disabled == false
                                select new GetBESBUInfoByClientIdResponseModel()
                                {
                                    ClientSBUId = cl.ClientSBUId,
                                    SBUId = sb.SBUID,
                                    ClientId = cl.ClientId,
                                    Name = sb.Name,
                                    description = sb.description,
                                    IsClientSBU = sb.IsClientSBU.HasValue ? sb.IsClientSBU : true,
                                    Disabled = cl.Disabled,
                                    CreatedBy = sb.CreatedBy
                                }).ToListAsync();

            return result;
        }

        public async Task ManageClientData(AddClientInfoSetupRequest oClient)
        {
            List<SqlParameter> sqlParameters = new()
            {
                new SqlParameter(Parameters.PARAM_CLIENTNAME, oClient.ClientInfoSetupModel.ClientName),
                new SqlParameter(Parameters.PARAM_VERTICALID, oClient.ClientInfoSetupModel.VerticalID),
                new SqlParameter(Parameters.PARAM_CLIENTDESC, oClient.ClientInfoSetupModel.Description),
                new SqlParameter(Parameters.PARAM_ENDDATE, oClient.ClientInfoSetupModel.EndDate),
                new SqlParameter(Parameters.PARAM_EXLSPECIFICCLIENT, oClient.ClientInfoSetupModel.EXLSpecificClient),
                new SqlParameter(Parameters.PARAM_DISABLED, oClient.ClientInfoSetupModel.Disabled),
                new SqlParameter(Parameters.PARAM_CREATEDBY, oClient.ClientInfoSetupModel.CreatedBy),
                new SqlParameter(Parameters.PARAM_ACTION, "Add"),
                new SqlParameter(Parameters.PARAM_CLIENTID, oClient.ClientInfoSetupModel.ClientId)
            };
            await AppDbContext.Database.ExecuteSqlRawAsync(StoredProcedures.SQL_MANAGE_CLIENT, sqlParameters);
        }

        public async Task ManageClientData(UpdateClientInfoSetupRequest oClient)
        {
            List<SqlParameter> sqlParameters = new()
            {
                new SqlParameter(Parameters.PARAM_CLIENTNAME, oClient.ClientInfoSetupModel.ClientName),
                new SqlParameter(Parameters.PARAM_VERTICALID, oClient.ClientInfoSetupModel.VerticalID),
                new SqlParameter(Parameters.PARAM_CLIENTDESC, oClient.ClientInfoSetupModel.Description),
                new SqlParameter(Parameters.PARAM_ENDDATE, oClient.ClientInfoSetupModel.EndDate),
                new SqlParameter(Parameters.PARAM_EXLSPECIFICCLIENT, oClient.ClientInfoSetupModel.EXLSpecificClient),
                new SqlParameter(Parameters.PARAM_DISABLED, oClient.ClientInfoSetupModel.Disabled),
                new SqlParameter(Parameters.PARAM_CREATEDBY, oClient.ClientInfoSetupModel.CreatedBy),
                new SqlParameter(Parameters.PARAM_ACTION, "Update"),
                new SqlParameter(Parameters.PARAM_CLIENTID, oClient.ClientInfoSetupModel.ClientId)
            };
            await AppDbContext.Database.ExecuteSqlRawAsync(StoredProcedures.SQL_MANAGE_CLIENT, sqlParameters);
        }

        public async Task<int> GetMaxClientId()
        {
            SqlParameter sqlParameterOut = new()
            {
                ParameterName = "@ReturnValue",
                DbType = DbType.Int32,
                Size = 20,
                Direction = ParameterDirection.Output
            };

            await AppDbContext.Database.ExecuteSqlRawAsync(StoredProcedures.SP_GETMAX_CLIENTID, sqlParameterOut);

            int lastInsertedId = (int)sqlParameterOut.Value;

            return lastInsertedId;
        }

        //public async Task<DataSet> GetClientList(GetBEClientInfoRequest request)
        //{
        //    List<SqlParameter> sqlParameters = new()
        //    {
        //        new SqlParameter(Parameters.PARAM_CLIENTNAME, request.ClientName),
        //        new SqlParameter(Parameters.PARAM_USERID,request.UserID),
        //        new SqlParameter(Parameters.PARAM_ACTIVECLIENTLIST, true)
        //    }

        //    //var result = AppDbContext.Database.SqlQuery<>();

        //    //var result = AppDbContext.Set<GetBEClientInfoResponse>().FromSqlRaw(StoredProcedures.SQL_SP_CLIENT, sqlParameters);

        //    return await GetDataSet(StoredProcedures.SQL_SP_CLIENT, sqlParameters);
        //}

        public async Task<DataSet> GetClientList(GetBEClientInfoRequest request)
        {
            List<SqlParameter> sqlParameters = new()
            {
                new SqlParameter(Parameters.PARAM_CLIENTNAME, request.ClientName),
                new SqlParameter(Parameters.PARAM_USERID,request.UserID),
                new SqlParameter(Parameters.PARAM_ACTIVECLIENTLIST, true)
            };

            //AppDbContext.Database.Sql()

            // var result = AppDbContext.Database.SqlQuery<>();

            //var result = AppDbContext.Set<GetBEClientInfoResponse>().FromSqlRaw(StoredProcedures.SQL_SP_CLIENT, sqlParameters);

            return await GetDataSet(StoredProcedures.SQL_SP_CLIENT, sqlParameters);
        }

        public async Task<List<GetBEClientInfoResponse>> GetClients(GetBEClientInfoRequest request)
        {
            var sqlParameters = new List<SqlParameter>
            {
                new SqlParameter(Parameters.PARAM_CLIENTNAME, request.ClientName),
                new SqlParameter(Parameters.PARAM_USERID, request.UserID),
                new SqlParameter(Parameters.PARAM_ACTIVECLIENTLIST, true)
            };

            //var clients = await AppDbContext.Set<GetBEClientInfoResponse>().FromSqlRaw($"EXEC Usp_CDS_GetClientList @ClientName,@UserID,@ActiveClientList", parameters: sqlParameters.ToArray()).ToListAsync();
            var clients = await AppDbContext.Set<GetBEClientInfoResponse>().FromSqlRaw(StoredProcedures.SQL_SP_CLIENT, parameters: sqlParameters.ToArray()).ToListAsync();
            return clients;
        }

        public async Task<List<GetBEClientInfoResponse>> GetClientsUsingDapper(GetBEClientInfoRequest request)
        {
            DynamicParameters parameterList = new DynamicParameters();
            //parameterList.Add("@ClientName", request.ClientName, DbType.String);
            //parameterList.Add("@UserID", request.UserID, DbType.Int32);
            //parameterList.Add("@ActiveClientList", true, DbType.Boolean);

            parameterList.Add(Parameters.PARAM_CLIENTNAME, request.ClientName, DbType.String);
            parameterList.Add(Parameters.PARAM_USERID, request.UserID, DbType.Int32);
            parameterList.Add(Parameters.PARAM_ACTIVECLIENTLIST, true, DbType.Boolean);

            var clients = await dapperUtilityObj.GetAllAsync<GetBEClientInfoResponse>("Usp_CDS_GetClientList", parameterList);
            return clients.ToList();
        }
    }

}
