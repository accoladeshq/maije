using Accolades.Maije.AppService.Dto;
using System;

namespace Accolades.Maije.Distributed.Tests.Mocks.Dto
{
    internal class CategoryDto : IIdentityDto<Guid>
    {
        public Guid Id { get; set; }
    }
}
