using System.Collections.Generic;

namespace Accolades.Maije.Crosscutting.Context
{
    public interface IRouteContext
    {
        /// <summary>
        /// Gets the route host
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Gets the route path
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Gets the route query
        /// </summary>
        IEnumerable<KeyValuePair<string, string>> Query { get; }

        /// <summary>
        /// Gets the route scheme
        /// </summary>
        string Scheme { get; }

        /// <summary>
        /// Gets the formatted route url
        /// </summary>
        /// <returns></returns>
        string GetFormattedUrl();
    }
}
