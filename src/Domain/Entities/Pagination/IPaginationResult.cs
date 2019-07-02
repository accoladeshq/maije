using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accolades.Maije.Domain.Entities
{
    public interface IPaginationResult<T>
    {
        /// <summary>
        /// Gets the paginated items
        /// </summary>
        [Required]
        IEnumerable<T> Items { get; }

        /// <summary>
        /// Gets the pagination information
        /// </summary>
        [Required]
        PaginationResultInfo Pagination { get; }

        /// <summary>
        /// Gets the pagination links
        /// </summary>
        [Required]
        IEnumerable<PaginationLink> Links { get; }
    }
}
