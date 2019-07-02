using Accolades.Maije.Crosscutting.Enums;
using System;

namespace Accolades.Maije.Domain.Entities
{
    public class PaginationLink
    {
        /// <summary>
        /// Initialize a <see cref="PaginationLink"/>
        /// </summary>
        /// <param name="href">The href</param>
        /// <param name="rel">The rel</param>
        /// <param name="method">The method</param>
        public PaginationLink(string href, string rel, HttpMethod method)
        {
            Href = href ?? throw new ArgumentNullException(nameof(href));
            Rel = rel ?? throw new ArgumentNullException(nameof(rel));
            Method = method;
        }

        /// <summary>
        /// Gets the link href
        /// </summary>
        public string Href { get; }

        /// <summary>
        /// Gets the link rel
        /// </summary>
        public string Rel { get; }

        /// <summary>
        /// Gets the link method
        /// </summary>
        public HttpMethod Method { get; }
    }
}
