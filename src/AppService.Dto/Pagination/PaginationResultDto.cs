using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accolades.Maije.AppService.Dto
{
    public class PaginationResultDto<T>
    {
        /// <summary>
        /// Gets or sets the pagination items
        /// </summary>
        [Required]
        public List<T> Items { get; set; }

        /// <summary>
        /// Gets or sets the pagination links
        /// </summary>
        [Required]
        public List<PaginationLinkDto> Links { get; set; }

        /// <summary>
        /// Gets or sets the pagination info
        /// </summary>
        [Required]
        public PaginationResultInfoDto Pagination { get; set; }
    }
}
