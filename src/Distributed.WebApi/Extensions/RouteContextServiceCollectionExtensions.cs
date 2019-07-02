using Accolades.Maije.Crosscutting.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Accolades.Maije.Distributed.WebApi.Extensions
{
    internal static class RouteContextServiceCollectionExtensions
    {
        /// <summary>
        /// Setup the context information
        /// </summary>
        /// <param name="services">The services</param>
        public static void AddMaijeRouteContext(this IServiceCollection services)
        {
            // https://github.com/aspnet/Announcements/issues/190
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<IRouteContext, RouteContext>(serviceProvider =>
            {
                var context = serviceProvider.GetRequiredService<IHttpContextAccessor>();

                if (context.HttpContext == null)
                {
                    // During migration the http context is null but IContextInfo is resolved.
                    // So we allow empty context info.
                    return new NoRouteContext();
                }

                var urlInfo = BuildUrlInfo(context);

                return urlInfo;
            });
        }

        /// <summary>
        /// Build <see cref="UrlInfo"/> from current http context
        /// </summary>
        /// <param name="context">The current context</param>
        /// <returns></returns>
        private static RouteContext BuildUrlInfo(IHttpContextAccessor context)
        {
            if (context.HttpContext == null)
            {
                return null;
            }

            var queryParameters = new Dictionary<string, string>();

            foreach (var parameter in context.HttpContext.Request.Query)
            {
                queryParameters.Add(parameter.Key, parameter.Value);
            }

            var urlInfo = new RouteContext(context.HttpContext.Request.Scheme, context.HttpContext.Request.Host.Value, context.HttpContext.Request.Path, queryParameters);

            return urlInfo;
        }
    }
}
