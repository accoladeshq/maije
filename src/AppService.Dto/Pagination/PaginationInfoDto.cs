using System.ComponentModel.DataAnnotations;

namespace Accolades.Maije.AppService.Dto
{
    public class PaginationInfoDto
    {
        /// <summary>
        /// Gets the pagination offset
        /// </summary>
        [Required]
        public int Offset { get; set; }

        /// <summary>
        /// Gets the pagination limit
        /// </summary>
        [Required]
        public int Limit { get; set; }
    }
}
