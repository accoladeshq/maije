using Accolades.Maije.Crosscutting.Context;
using System;
using System.Linq;

namespace Accolades.Maije.Crosscutting.Extensions
{
    public static class RouteContextExtensions
    {
        /// <summary>
        /// The limit parameter name
        /// </summary>
        public const string LimitParameterName = "limit";

        /// <summary>
        /// The offset parameter name
        /// </summary>
        public const string OffsetParameterName = "offset";

        /// <summary>
        /// Gets an update pagination formatted url
        /// </summary>
        /// <param name="urlInfo">The url info</param>
        /// <param name="updatedOffset">The offset to use</param>
        /// <param name="updatedLimit">The limit to use</param>
        /// <returns></returns>
        public static string GetUpdatedPaginationFormattedUrl(this IRouteContext urlInfo, int updatedOffset, int updatedLimit)
        {
            var parameters = urlInfo.Query.ToDictionary(v => v.Key, e => e.Value, StringComparer.OrdinalIgnoreCase);

            parameters[OffsetParameterName] = updatedOffset.ToString();
            parameters[LimitParameterName] = updatedLimit.ToString();

            var newUrlInfo = new RouteContext(urlInfo.Scheme, urlInfo.Host, urlInfo.Path, parameters.ToList());

            return newUrlInfo.GetFormattedUrl();
        }
    }
}
