using Accolades.Maije.AppService.Dto;

namespace Accolades.Maije.SampleApi.Dto
{
    /// <summary>
    /// Gets the value representation
    /// </summary>
    public class ValueDto : IIdentityDto<int>
    {
        /// <summary>
        /// Gets or sets the value identifier
        /// </summary>
        public int Id { get; set; }
    }
}
