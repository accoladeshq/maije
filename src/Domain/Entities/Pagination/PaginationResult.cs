using System;
using System.Collections.Generic;
using System.Linq;

namespace Accolades.Maije.Domain.Entities
{
    public class PaginationResult<T> : IPaginationResult<T>
    {
        /// <summary>
        /// Initialize a default <see cref="PaginationList{T}"/>
        /// </summary>
        /// <param name="items">The page items</param>
        public PaginationResult(IEnumerable<T> items, PaginationResultInfo pagination, IEnumerable<PaginationLink> links)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
            Pagination = pagination ?? throw new ArgumentNullException(nameof(pagination));
            Links = links.ToList() ?? throw new ArgumentNullException(nameof(links));
        }

        /// <summary>
        /// Initialize a <see cref="PaginationResult{T}"/>
        /// </summary>
        /// <param name="items">The page items</param>
        /// <param name="totalItems">The total number of items</param>
        /// <param name="offset">The current offset</param>
        /// <param name="limit">The current limit</param>
        /// <param name="links"></param>
        public PaginationResult(IEnumerable<T> items, int offset, int totalItems, int limit, IEnumerable<PaginationLink> links) : this(items, new PaginationResultInfo(offset, limit, totalItems), links)
        {
        }

        /// <summary>
        /// Gets the paginated items
        /// </summary>
        public IEnumerable<T> Items { get; }

        /// <summary>
        /// Gets the pagination information
        /// </summary>
        public PaginationResultInfo Pagination { get; }

        /// <summary>
        /// Gets the pagination links
        /// </summary>
        public IEnumerable<PaginationLink> Links { get; }
    }
}
