using Accolades.Maije.Domain.Entities;

namespace Accolades.Maije.Distributed.Tests.Mocks.Entities
{
    internal class Post : IIdentity<string>
    {
        public string Id { get; set; }
    }
}
