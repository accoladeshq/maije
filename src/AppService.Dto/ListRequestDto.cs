using Accolades.Maije.Crosscutting.Enums;

namespace Accolades.Maije.AppService.Dto
{
    public class ListRequestDto
    {
        /// <summary>
        /// Gets the terms we are looking for
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Gets the order column nam
        /// </summary>
        public string OrderColumnName { get; set; }

        /// <summary>
        /// Gets the order type
        /// </summary>
        public OrderType OrderType { get; set; }
    }
}
