using Accolades.Maije.AppService.Dto;

namespace Accolades.Maije.SampleApi.Dto
{
    /// <summary>
    /// Gets the user representation
    /// </summary>
    public class UserDto : IIdentityDto<int>
    {
        /// <summary>
        /// Gets or sets the value identifier
        /// </summary>
        public int Id { get; set; }
    }
}
