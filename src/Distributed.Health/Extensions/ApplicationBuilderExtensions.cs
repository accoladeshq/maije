using Accolades.Maije.Distributed.Health.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Accolades.Maije.Distributed.Health
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseMaijeHealthChecks(this IApplicationBuilder applicationBuilder)
        {
            var healthOptions = new HealthCheckOptions
            {
                ResponseWriter = WriteHealthResponse
            };

            applicationBuilder.UseHealthChecks("/status", healthOptions);
        }

        public async static Task WriteHealthResponse(HttpContext httpContext, HealthReport healthReport)
        {
            httpContext.Response.ContentType = "application/json";

            var jsonSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
            jsonSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            var result = JsonConvert.SerializeObject(healthReport.ToDto(), jsonSettings);

            await httpContext.Response.WriteAsync(result);
        }
    }
}
