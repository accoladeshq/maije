using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Linq;

namespace Accolades.Maije.Distributed.Health.Extensions
{
    internal static class HealthReportExtensions
    {
        public static HealthReportDto ToDto(this HealthReport healthReport)
        {
            HealthStatus status;

            switch (healthReport.Status)
            {
                case Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy:
                    status = HealthStatus.Fail;
                    break;
                case Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded:
                    status = HealthStatus.Warn;
                    break;
                case Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy:
                    status = HealthStatus.Pass;
                    break;
                default:
                    status = HealthStatus.Fail;
                    break;
            }

            return new HealthReportDto
            {
                Status = status,
                Details = healthReport.Entries.Select(e => new ComponentReportDto { ComponentId = e.Key }).ToDictionary(h => h.ComponentId)
            };
        }
    }
}
