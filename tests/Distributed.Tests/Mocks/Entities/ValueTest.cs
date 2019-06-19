using Accolades.Maije.Domain.Entities;

namespace Accolades.Maije.Distributed.Tests.Mocks.Entities
{
    public class ValueTest : IIdentity<int>
    {
        public ValueTest(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
