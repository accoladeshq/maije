using Accolades.Maije.Infrastructure.Exceptions;
using Accolades.Maije.Infrastructure.Tests.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure.Tests
{
    public partial class RepositoryBaseTests
    {
        [TestMethod]
        public async Task Should_ReturnTestList_When_QueryDbSet()
        {
            var repository = GetDefaultRepository();

            var items = await repository.GetItemsAsync();

            items.Should().HaveCount(Snapshot.Tests.Count);
        }

        [TestMethod]
        public async Task Should_ReturnEntityWithId_When_IdExists()
        {
            var repository = GetDefaultRepository();

            var item = await repository.GetItemByIdAsync(Snapshot.Tests.GetExistingId());

            item.Should().NotBeNull();
        }

        [ExpectedException(typeof(InfrastructureException))]
        [TestMethod]
        public async Task Should_RaiseException_When_IdNotExists()
        {
            var repository = GetDefaultRepository();

            await repository.GetItemByIdAsync(Snapshot.Tests.GetNonExistingId());
        }
    }
}
