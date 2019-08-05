using Accolades.Maije.Crosscutting.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Accolades.Maije.Distributed.Documentation.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Add swagger to the application
        /// </summary>
        /// <param name="app">The application to build</param>
        /// <param name="provider">The version description provider</param>
        /// <param name="maijeConfiguration">The api configuration</param>
        public static void UseMaijeDocumentation(this IApplicationBuilder app)
        {
            var configuration = app.ApplicationServices.GetRequiredService<IOptions<MaijeConfiguration>>().Value;

            app.UseSwagger();

            app.UseSwaggerUI(o =>
            {
                if (configuration.OAuth2Enabled)
                {
                    o.OAuth2RedirectUrl($"{configuration.BaseUrl}/swagger/oauth2-redirect.html");
                    o.OAuthClientId(configuration.Authentication.ClientId);
                }

                var apiVersionDescriptorProvider = app.ApplicationServices.GetService<IApiVersionDescriptionProvider>();

                // build a swagger endpoint for each discovered API version
                foreach (var description in apiVersionDescriptorProvider.ApiVersionDescriptions)
                {
                    o.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                }
            });
        }
    }
}
