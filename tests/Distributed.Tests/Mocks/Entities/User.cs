using Accolades.Maije.Domain.Entities;

namespace Accolades.Maije.Distributed.Tests.Mocks.Entities
{
    internal class User : IIdentity<int>, IDeactivatable
    {
        public bool IsActive { get; set; }

        public int Id { get; set; }
    }
}
