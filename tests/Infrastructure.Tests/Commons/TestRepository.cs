using Accolades.Maije.Tests.Commons.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accolades.Maije.Infrastructure.Tests.Commons
{
    internal class TestRepository : RepositoryBase<Test, int>
    {
        /// <summary>
        /// Initialize a new <see cref="TestRepository"/>
        /// </summary>
        /// <param name="databaseContext"></param>
        public TestRepository(DbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
