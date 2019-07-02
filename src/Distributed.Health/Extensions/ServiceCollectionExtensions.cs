using Microsoft.Extensions.DependencyInjection;

namespace Accolades.Maije.Distributed.Health
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMaijeHealthChecks(this IServiceCollection services)
        {
            services.AddHealthChecks();
        }
    }
}
