using System;

namespace Accolades.Maije.AppService.Dto
{
    public interface IIdentityDto<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// Gets or sets the entity dto identifier
        /// </summary>
        TIdentifier Id { get; set; }
    }


    public interface IIdentityDto : IIdentityDto<Guid>
    {
    }
}
