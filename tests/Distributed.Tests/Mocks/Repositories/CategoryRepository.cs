using Accolades.Maije.Distributed.Tests.Mocks.Entities;
using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Domain.Services;
using Accolades.Maije.Infrastructure;
using System;

namespace Accolades.Maije.Distributed.Tests.Mocks.Repositories
{
    internal class CategoryRepository : MaijeRepository<Category, Guid>, IMaijeRepository<Category, Guid>
    {
        public CategoryRepository(IMaijeDbContext databaseContext, IPaginationDomainService paginationDomainService) : base(databaseContext, paginationDomainService)
        {
        }
    }
}
