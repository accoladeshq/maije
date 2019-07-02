using Accolades.Maije.Domain.Entities;
using System.Collections.Generic;

namespace Accolades.Maije.Domain.Services
{
    public interface IPaginationDomainService
    {
        /// <summary>
        /// Validate the pagination
        /// </summary>
        /// <param name="paginationRequest">The pagination request to validate</param>
        void ValidatePagination(PaginationRequest paginationRequest);

        /// <summary>
        /// Gets the current request pagination links
        /// </summary>
        /// <returns></returns>
        IEnumerable<PaginationLink> GetCurrentRequestPaginationLinks(int offset, int limit, int count);
    }
}
