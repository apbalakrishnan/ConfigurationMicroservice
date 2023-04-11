using Configuration.Common.Utility;
using Configuration.Repository.VerticalInfoSetup;
using Microsoft.AspNetCore.Mvc;

namespace Configuration.Api.Controllers
{

    [ApiController]
    public class VerticalMasterController : ControllerBase
    {
        private readonly IVerticalService _repositoryVertical;
        public VerticalMasterController(IVerticalService _repositaryVertical)
        {
            this._repositoryVertical = _repositaryVertical;
        }

        /// <summary>
        /// Get Vertical List
        /// </summary>
        /// <param name="IsActiveVertical"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(AppConstants.ApiVersion + "Vertical/GetVerticals")]
        public async Task<IActionResult> GetVerticalList([FromQuery] bool IsActiveVertical)
        {
            var result = await _repositoryVertical.GetVerticalList(IsActiveVertical);
            return Ok(result);
        }
    }
}
