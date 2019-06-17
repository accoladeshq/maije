using Accolades.Maije.Infrastructure.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Accolades.Maije.Infrastructure.Tests.Repositories
{
    public class ActivateTestRepository : DeactivatableRepositoryBase<ActivableTest, Guid>
    {
        /// <summary>
        /// Initialize a new <see cref="ActivateTestRepository"/>
        /// </summary>
        /// <param name="databaseContext"></param>
        public ActivateTestRepository(DbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
