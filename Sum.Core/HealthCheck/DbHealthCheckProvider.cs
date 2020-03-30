using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Sum.Core.HealthCheck
{
    public static class DbHealthCheckProvider
    {
        public static HealthCheckResult Check(string connectionString)
        {

            return HealthCheckResult.Healthy();
        }
    }
}