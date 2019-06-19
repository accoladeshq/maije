using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accolades.Maije.Distributed.Tests.Mocks.Data
{
    public class TestDbContext : MaijeDbContext, IMaijeDbContext
    {
        public TestDbContext()
        {
        }

        public TestDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
