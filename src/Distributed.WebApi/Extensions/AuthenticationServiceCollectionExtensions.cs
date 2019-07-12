using Accolades.Maije.Crosscutting.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Accolades.Maije.Distributed.WebApi.Extensions
{
    internal static class AuthenticationServiceCollectionExtensions
    {
        /// <summary>
        /// Setup the authentication
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <param name="apiConfiguration">The api configuration</param>
        public static void AddMaijeAuthentication(this IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetRequiredService<IOptions<MaijeConfiguration>>().Value;

            var authConfiguration = configuration.Authentication;
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, o =>
            {
                o.Audience = authConfiguration.FrontOfficeClient.Audience;
                o.Authority = authConfiguration.AuthenticationUrl;
                o.RequireHttpsMetadata = false;
                o.TokenValidationParameters.NameClaimType = configuration.NameClaimType;
            });
        }
    }
}
