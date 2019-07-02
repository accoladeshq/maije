using System;

namespace Accolades.Maije.AppService.Dto
{
    public interface IUpdatableDto<TIdentifier> : IIdentityDto<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
    }
}
