using Accolades.Maije.Crosscutting.Configurations;
using Accolades.Maije.Crosscutting.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accolades.Maije.Distributed.WebApi.Extensions
{
    internal static class UserContextServiceCollection
    {
        public static void AddMaijeUserContext(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>(serviceProvider =>
            {
                var configuration = serviceProvider.GetService<IOptions<MaijeConfiguration>>().Value;
                var context = serviceProvider.GetRequiredService<IHttpContextAccessor>();

                if (context.HttpContext == null)
                {
                    // During migration the http context is null but IContextInfo is resolved.
                    // So we allow empty context info.
                    return new UserContext(Guid.Empty, new Dictionary<string, string>());
                }

                var currentUser = context.HttpContext?.User?.GetUserName();

                Dictionary<string, string> claimsToInclude = new Dictionary<string, string>();

                if(configuration.Authentication.FrontOfficeClient.Claims != null)
                    claimsToInclude = context.HttpContext?.User?.Claims?.Where(c => configuration.Authentication.FrontOfficeClient.Claims.Contains(c.Type)).ToDictionary(c => c.Type, c => c.Value);

                return new UserContext(currentUser.Value, claimsToInclude);
            });
        }
    }
}
