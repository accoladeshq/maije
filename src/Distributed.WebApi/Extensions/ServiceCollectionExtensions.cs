using Accolades.Maije.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Accolades.Maije.Distributed.WebApi.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Register application generics. Resolve all controllers and register the needed layers
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddMaijeGenerics(this IServiceCollection services, params Assembly[] assemblies)
        {
            var allExistingTypes = assemblies.SelectMany(a => a.GetTypes());

            var dbContextType = allExistingTypes.GetMaijeDbContext();
            services.AddScoped(typeof(IMaijeDbContext), dbContextType);

            var controllers = allExistingTypes.Where(t => t.IsMaijeController()).ToList();

            foreach (var controllerType in controllers)
            {
                var controllerRequiredServices = allExistingTypes.GetControllerRequiredServices(controllerType);

                foreach (var requireService in controllerRequiredServices)
                {
                    services.AddScoped(requireService.Key, requireService.Value);
                }
            }
        }
    }
}
