using Configuration.Common.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Configuration.Api.Controllers
{
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        private readonly HealthCheckService _service;
        public HealthCheckController(HealthCheckService service)
        {
            _service = service;
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(AppConstants.ApiVersion + "HealthCheck/Welcome")]
        public async Task<IActionResult> Welcome()
        {
            return Ok(await Task.FromResult($"Welcome to Microservice world! {DateTime.Now}"));
        }


        [HttpGet]
        [Route(AppConstants.ApiVersion + "HealthCheck/CheckHealth")]
        public async Task<IActionResult> Get()
        {
            var report = await _service.CheckHealthAsync();
            string json = System.Text.Json.JsonSerializer.Serialize(report);

            if (report.Status == HealthStatus.Healthy)
                return Ok(json);
            return NotFound("Service unavailable");
        }

        /*
        [HttpGet]
        [Route(AppConstants.ApiVersion + "HealthCheck/BuildHealthCheckApi")]
        public async Task<IActionResult> HealthCheckApi1()
        {

            return Ok(new string[] { "Health Check Completed", "Application is Stable" });
        }

        [HttpGet]
        [Route(AppConstants.ApiVersion + "HealthCheck/Version")]
        public async Task<IActionResult> Version()
        {
            return Ok(System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString());
        }

        [HttpGet]
        [Route(AppConstants.ApiVersion + "HealthCheck/BuildHealthCheckApi")]
        public async Task<IActionResult> GetEnvironment()
        {
            return Ok(Environment.EnvironmentName);
        }

        [HttpGet]
        [Route(AppConstants.ApiVersion + "HealthCheck/BuildInfo")]
        public async Task<IActionResult> GetEnvironment()
        {
            string buildInfo = string.Empty;
            if (System.IO.File.Exists("."))
            {
                buildInfo=System.IO.File.ReadAllText(".");  
            }
            else
            {
                buildInfo = "Build Information does not exist";
            }
            return Ok(Environment.EnvironmentName);
        }*/
    }
}
