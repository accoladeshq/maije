using System;
using System.Threading;
using System.Threading.Tasks;
using Accolades.Maije.Domain.Contracts;

namespace Accolades.Maije.Distributed.Tests.Mocks.Data
{
    public class TestDbContext : IMaijeDbContext
    {
        public void Dispose()
        {
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
