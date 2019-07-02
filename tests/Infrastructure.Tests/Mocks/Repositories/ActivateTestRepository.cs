using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Domain.Services;
using Accolades.Maije.Infrastructure.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Accolades.Maije.Infrastructure.Tests.Repositories
{
    public class ActivateTestRepository : DeactivatableMaijeRepository<ActivableTest, int>
    {
        /// <summary>
        /// Initialize a new <see cref="ActivateTestRepository"/>
        /// </summary>
        /// <param name="databaseContext"></param>
        public ActivateTestRepository(IMaijeDbContext databaseContext, IPaginationDomainService paginationDomainService) : base(databaseContext, paginationDomainService)
        {
        }
    }
}
