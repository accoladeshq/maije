using Accolades.Maije.AppService.Dto;

namespace Accolades.Maije.Distributed.Tests.Mocks.Dto
{
    internal class PostDto : IIdentityDto<string>
    {
        public string Id { get; set; }
    }
}
