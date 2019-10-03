using Accolades.Maije.Domain.Entities;
using System;

namespace Accolades.Maije.Distributed.Tests.Mocks.Entities
{
    internal class Category : IIdentity<Guid>
    {
        public Guid Id { get; set; }
    }
}
