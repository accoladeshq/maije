using Accolades.Maije.Crosscutting.Configurations;
using Accolades.Maije.Distributed.Documentation.Filters;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace Accolades.Maije.Distributed.Documentation.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Setup swagger for application
        /// </summary>
        /// <param name="services">The service where we register swagger</param>
        /// <param name="apiConfiguration">The swagger configuration</param>
        public static void AddMaijeDocumentation(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IOptions<MaijeConfiguration>>().Value;

            services.AddSwaggerGen(options =>
            {
                if (configuration.OAuth2Enabled)
                {
                    ConfigureAuthentication(options, configuration.Authentication);
                }

                // resolve the IApiVersionDescriptionProvider service
                // note: that we have to build a temporary service provider here because one has not been created yet
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                // add a swagger document for each discovered API version
                // note: you might choose to skip or document deprecated API versions differently
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.DescribeAllParametersInCamelCase();
                    options.SwaggerDoc(description.GroupName, SwaggerHelper.CreateInfoForApiVersion(description, configuration));
                }

                options.CustomSchemaIds(SwaggerHelper.GetCustomSchemaId);

                // Configure AllOf property of the swagger.json
                options.SchemaFilter<SubtypeOfAttributeFilter>();

                // add a custom operation filter which sets default values
                options.OperationFilter<DefaultOperationsFilter>();

                // workaround with ApiExplorer who don't lower case controller name
                options.DocumentFilter<LowercaseDocumentFilter>();

                // integrate xml comments
                options.IncludeXmlComments(SwaggerHelper.GetXmlCommentsFilePath());

            });
        }

        /// <summary>
        /// Configure the swagger authentication
        /// </summary>
        /// <param name="options">The swagger gen options</param>
        /// <param name="authenticationConfiguration">The authentication configuration</param>
        private static void ConfigureAuthentication(SwaggerGenOptions options, AuthenticationConfiguration authenticationConfiguration)
        {
            options.AddSecurityDefinition("Swagger", new OAuth2Scheme
            {
                Type = "oauth2",
                Flow = authenticationConfiguration.FrontOfficeClient.Flow.ToString().ToLower(),
                AuthorizationUrl = authenticationConfiguration.AuthorizeUrl,
                TokenUrl = authenticationConfiguration.TokenUrl,
                Scopes = authenticationConfiguration.FrontOfficeClient.Scopes
            });

            options.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Swagger", authenticationConfiguration.FrontOfficeClient.Scopes?.Select(kvp => kvp.Key )}
                });
        }
    }
}
