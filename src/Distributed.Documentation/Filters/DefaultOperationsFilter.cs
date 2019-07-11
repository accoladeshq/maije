using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Accolades.Maije.Distributed.Documentation.Filters
{
    // From https://github.com/Microsoft/aspnet-api-versioning/wiki/Swashbuckle-Integration
    internal class DefaultOperationsFilter : IOperationFilter
    {
        /// <summary>
        /// Apply filter to the operation
        /// </summary>
        /// <param name="operation">The operation we need apply the filter to</param>
        /// <param name="context">The operation context</param>
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
            {
                return;
            }

            foreach (var parameter in operation.Parameters.OfType<NonBodyParameter>())
            {
                var description = context.ApiDescription
                                         .ParameterDescriptions
                                         .First(p => p.Name.ToUpper() == parameter.Name.ToUpper());
                var routeInfo = description.RouteInfo;

                if (parameter.Description == null)
                {
                    parameter.Description = description.ModelMetadata?.Description;
                }

                if (routeInfo == null)
                {
                    continue;
                }

                if (parameter.Default == null)
                {
                    parameter.Default = routeInfo.DefaultValue;
                }

                parameter.Required |= !routeInfo.IsOptional;
            }
        }
    }
}
