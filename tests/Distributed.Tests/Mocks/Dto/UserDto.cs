using Accolades.Maije.AppService.Dto;

namespace Accolades.Maije.Distributed.Tests.Mocks.Dto
{
    internal class UserDto : IIdentityDto<int>
    {
        public int Id { get; set; }
    }
}
