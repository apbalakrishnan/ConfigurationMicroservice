using Configuration.Common.Messages;
using Configuration.Common.Utility;
using Configuration.DomainModel.ClientInfoSetup;
using Configuration.DomainModel.ClientInfoSetup.Request;
using Configuration.Repository.ClientInfoSetup;
using Microsoft.AspNetCore.Mvc;

namespace Configuration.Api.Controllers
{

    [ApiController]
    public class ClientInfoSetupController : ControllerBase
    {
        private readonly IClientInfoSetupService _repositoryClient;
        public ClientInfoSetupController(IClientInfoSetupService _repositoryClient)
        {
            this._repositoryClient = _repositoryClient;
        }

        /// <summary>
        /// Get SBUListBasedOnClient
        /// </summary>
        /// <param name="iclientid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(AppConstants.ApiVersion + "ClientInfoSetup/GetSBUListBasedOnClient")]
        public async Task<IActionResult> GetSBUListbasedONClient([FromQuery] int iclientid)
        {
            var result = await _repositoryClient.GetSBUListbasedONClient(iclientid);
            return Ok(result);
        }

        /// <summary>
        /// Search Client Info Setup
        /// </summary>
        /// <param name="AddClientInfoSetupRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(AppConstants.ApiVersion + "ClientInfoSetup/SearchClient")]
        public async Task<IActionResult> SearchClient([FromBody] GetBEClientInfoRequest request)
        {
            var result = await _repositoryClient.GetClientList(request);

            return Ok(result);
        }

        /// <summary>
        /// Post Client Info Setup
        /// </summary>
        /// <param name="AddClientInfoSetupRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(AppConstants.ApiVersion + "ClientInfoSetup/AddNewClient")]
        public async Task<IActionResult> AddNewClient([FromBody] AddClientInfoSetupRequest request)
        {
            var result = await _repositoryClient.InsertData(request);
            return Ok(CustomSuccessResponse.GetSuccessResponse<CommonResponse, int, string>(result, AppConstants.SuccessMessage.Inserted.ToString()));
        }

        /// <summary>
        /// Update Client Info Setup
        /// </summary>
        /// <param name="AddClientInfoSetupRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(AppConstants.ApiVersion + "ClientInfoSetup/UpdateClient")]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientInfoSetupRequest request)
        {
            var result = await _repositoryClient.UpdateData(request);
            return Ok(CustomSuccessResponse.GetSuccessResponse<CommonResponse, int, string>(result, AppConstants.SuccessMessage.Inserted.ToString()));
        }
    }
}
