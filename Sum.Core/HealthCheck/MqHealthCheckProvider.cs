using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Sum.Core.HealthCheck
{
    public static class MqHealthCheckProvider
    {
        public static HealthCheckResult Check(string mqUri)
        {
            return HealthCheckResult.Healthy();
        }
    }
}   