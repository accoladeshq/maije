using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace Accolades.Maije.Distributed.WebApi.Extensions
{
    internal static class VersioningServiceCollectionExtensions
    {
        /// <summary>
        /// Add versioning mechanism
        /// </summary>
        /// <param name="services"></param>
        public static void AddMaijeVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(o => o.ApiVersionReader = new UrlSegmentApiVersionReader());
            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";

                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                // can also be used to control the format of the API version in route templates
                options.SubstituteApiVersionInUrl = true;
            });
        }
    }
}
