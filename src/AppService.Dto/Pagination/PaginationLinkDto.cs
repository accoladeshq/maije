
using Accolades.Maije.Crosscutting.Enums;

namespace Accolades.Maije.AppService.Dto
{
    public class PaginationLinkDto
    {
        /// <summary>
        /// Gets the link href
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Gets the link rel
        /// </summary>
        public string Rel { get; set; }

        /// <summary>
        /// Gets the link method
        /// </summary>
        public HttpMethod Method { get; set; }
    }
}
