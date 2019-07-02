using System;

namespace Accolades.Maije.Domain.Entities
{
    public class PaginationRequest : PaginationInfo
    {
        /// <summary>
        /// Initialize a new <see cref="PaginationRequest"/>
        /// </summary>
        /// <param name="offset">The pagination request offset</param>
        /// <param name="limit">The pagination request limit</param>
        /// <param name="order">The pagination request order</param>
        /// <param name="search">The search term</param>
        public PaginationRequest(int offset, int limit, Order order, string search) : base(offset, limit)
        {
            Order = order ?? throw new ArgumentNullException(nameof(order));
            Search = search ?? string.Empty;
        }

        /// <summary>
        /// Gets the pagination request order
        /// </summary>
        public Order Order { get; }

        /// <summary>
        /// Gets the pagination request search term
        /// </summary>
        public string Search { get; }
    }
}
