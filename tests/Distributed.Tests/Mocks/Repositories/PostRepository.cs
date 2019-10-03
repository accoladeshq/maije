using Accolades.Maije.Distributed.Tests.Mocks.Entities;
using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Domain.Services;
using Accolades.Maije.Infrastructure;

namespace Accolades.Maije.Distributed.Tests.Mocks.Repositories
{
    internal interface IPostRepository : IMaijeRepository<Post, string> { }

    internal class PostRepository : MaijeRepository<Post, string>, IPostRepository
    {
        public PostRepository(IMaijeDbContext databaseContext, IPaginationDomainService paginationDomainService) : base(databaseContext, paginationDomainService)
        {
        }
    }
}
