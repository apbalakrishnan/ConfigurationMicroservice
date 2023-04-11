using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Configuration.Api
{
    public class CustomHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("The service is up and running."));
            }
            catch (Exception)
            {
                return Task.FromResult(
                    new HealthCheckResult(
                        context.Registration.FailureStatus, "The service is down."));
            }
        }

        public static Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            var hEntry = healthReport.Entries.Values.FirstOrDefault();
            return context.Response.WriteAsync($"{hEntry.Status}=>{hEntry.Description}");
        }
    }

    public class ConfigurationHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(HealthCheckResult.Healthy());
        }
    }

    public class ConfigurationHealthCheckWithParam : IHealthCheck
    {
        private readonly string _param;

        public ConfigurationHealthCheckWithParam(string param)
        {
            _param = param;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(HealthCheckResult.Healthy(description: _param));
        }

        public static Task WriteResponse(HttpContext context, HealthReport healthReport)
        {
            var hEntry = healthReport.Entries.Values.FirstOrDefault();
            return context.Response.WriteAsync($"{hEntry.Status}=>{hEntry.Description}");
        }
    }
}
