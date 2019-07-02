using System;
using System.Collections.Generic;

namespace Accolades.Maije.Crosscutting.Context
{
    public class NoRouteContext : RouteContext
    {
        public NoRouteContext() : base(string.Empty, string.Empty, string.Empty)
        {
        }
    }

    public class RouteContext : IRouteContext
    {
        /// <summary>
        /// Initialize an <see cref="UrlInfo"/>
        /// </summary>
        /// <param name="scheme">The scheme of the url</param>
        /// <param name="host">The host of the url</param>
        /// <param name="path">The path of the url</param>
        /// <param name="query">The query (parameters) of the url</param>
        public RouteContext(string scheme, string host, string path, IEnumerable<KeyValuePair<string, string>> query)
        {
            Scheme = scheme ?? throw new ArgumentNullException(scheme);
            Host = host ?? throw new ArgumentNullException(host);
            Path = path;
            Query = query;
        }

        /// <summary>
        /// Initialize an <see cref="UrlInfo"/>
        /// </summary>
        /// <param name="scheme">The url scheme</param>
        /// <param name="host">The url host</param>
        /// <param name="path">The url path</param>
        public RouteContext(string scheme, string host, string path) : this(scheme, host, path, new Dictionary<string, string>())
        {
        }

        /// <summary>
        /// Gets the url scheme
        /// </summary>
        public string Scheme { get; }

        /// <summary>
        /// Gets the url host
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Gets the url path
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets the query parameters
        /// </summary>
        public IEnumerable<KeyValuePair<string, string>> Query { get; }

        /// <summary>
        /// Gets formatted url based on this url information
        /// </summary>
        /// <returns></returns>
        public string GetFormattedUrl()
        {
            var formatQuery = GetFormattedParameters();

            if (string.IsNullOrEmpty(formatQuery))
                return $"{Scheme}://{Host}{Path}";

            return $"{Scheme}://{Host}{Path}?{formatQuery}";
        }

        /// <summary>
        /// Format query parameters
        /// </summary>
        /// <returns></returns>
        private string GetFormattedParameters()
        {
            var parametersList = new List<string>();

            foreach (var item in Query)
            {
                parametersList.Add(item.Key + "=" + item.Value);
            }

            return string.Join("&", parametersList);
        }
    }
}
