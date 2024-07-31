using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace core_web_api_fundamentals.api;

public class MyHealthCheck(HealthCheckResult healthCheckResult) : IHealthCheck
{
    private HealthCheckResult HealthCheckResult { get; } = healthCheckResult;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        return Task.FromResult(HealthCheckResult);
    }
}