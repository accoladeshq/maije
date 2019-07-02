using Accolades.Maije.Crosscutting.Context;
using Accolades.Maije.Crosscutting.Enums;
using Accolades.Maije.Crosscutting.Exceptions;
using Accolades.Maije.Crosscutting.Extensions;
using Accolades.Maije.Domain.Entities;
using System.Collections.Generic;

namespace Accolades.Maije.Domain.Services
{
    public class PaginationDomainService : IPaginationDomainService
    {
        /// <summary>
        /// The maximum limit
        /// </summary>
        private const int MaxLimit = 100;

        /// <summary>
        /// The current route context
        /// </summary>
        private readonly IRouteContext _routeContext;

        /// <summary>
        /// Initialize <see cref="PaginationDomainService"/>
        /// </summary>
        /// <param name="contextInfo">The current route context</param>
        public PaginationDomainService(IRouteContext contextInfo)
        {
            _routeContext = contextInfo;
        }

        /// <summary>
        /// Validate the pagination
        /// </summary>
        /// <param name="paginationRequest">The pagination request to validate</param>
        public void ValidatePagination(PaginationRequest paginationRequest)
        {
            if (paginationRequest.Offset < 0) throw new BusinessException("The offset must be positive", 100);
            if (paginationRequest.Limit <= 0) throw new BusinessException("The limit cannot be lesser than 1", 101);

            if (paginationRequest.Limit > MaxLimit) throw new BusinessException($"You cannot get more than {MaxLimit} at a time", 102);
        }

        /// <summary>
        /// Gets the current request pagination links
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PaginationLink> GetCurrentRequestPaginationLinks(int offset, int limit, int count)
        {
            var links = new List<PaginationLink>();

            var selfLink = new PaginationLink(_routeContext.GetFormattedUrl(), "self", HttpMethod.Get);
            links.Add(selfLink);


            var previousLink = GetPreviousLink(offset, limit);
            if (previousLink != null) links.Add(previousLink);

            var nextLink = GetNextLink(offset, limit, count);
            if (nextLink != null) links.Add(nextLink);

            return links;
        }

        /// <summary>
        /// Gets the previous link
        /// </summary>
        /// <param name="offset">The current offset</param>
        /// <param name="limit">The current limit</param>
        /// <param name="count">The total items</param>
        /// <returns></returns>
        private PaginationLink GetPreviousLink(int offset, int limit)
        {
            if (offset == 0) return null;

            int previousLimit, previousOffset;

            if (offset < limit)
            {
                previousOffset = 0;
                previousLimit = offset;
            }
            else
            {
                previousOffset = offset - limit;
                previousLimit = limit;
            }

            return new PaginationLink(_routeContext.GetUpdatedPaginationFormattedUrl(previousOffset, previousLimit), "previous", HttpMethod.Get);
        }

        /// <summary>
        /// Gets the next link
        /// </summary>
        /// <param name="offset">The pagination offset</param>
        /// <param name="limit">The pagination limit</param>
        /// <param name="count">The total of items</param>
        /// <returns></returns>
        private PaginationLink GetNextLink(int offset, int limit, int count)
        {
            if (offset >= count || offset + limit >= count) return null;

            int newLimit, newOffset;

            newOffset = offset + limit;

            if (newOffset + limit > count)
            {
                newLimit = newOffset - count;
            }
            else
            {
                newLimit = limit;
            }

            return new PaginationLink(_routeContext.GetUpdatedPaginationFormattedUrl(newOffset, newLimit), "next", HttpMethod.Get);
        }
    }
}
