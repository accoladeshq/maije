using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Infrastructure.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accolades.Maije.Infrastructure.Tests.Repositories
{
    internal class TestRepository : MaijeRepository<Test, int>
    {
        /// <summary>
        /// Initialize a new <see cref="TestRepository"/>
        /// </summary>
        /// <param name="databaseContext"></param>
        public TestRepository(IMaijeDbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
