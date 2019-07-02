using Microsoft.Extensions.DependencyInjection;

namespace Accolades.Maije.Distributed.Health
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMaijeHealthChecks(this IServiceCollection services)
        {
            // https://tools.ietf.org/html/draft-inadarei-api-health-check-02
            services.AddHealthChecks()
                .AddCheck<MaijeDbContextHealthCheck>("Database");
        }
    }
}
