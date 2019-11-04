using Accolades.Maije.Crosscutting.Enums;
using Accolades.Maije.Crosscutting.Exceptions;
using Accolades.Maije.Domain.Entities;
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

            var items = await repository.GetItemsAsync(GetDefaultAscendingListRequest());

            items.Should().HaveCount(Snapshot.Tests.Count);
        }

        [TestMethod]
        public async Task Should_ReturnAscendingOrderedList_When_QueryDbSet()
        {
            var repository = GetDefaultRepository();

            var items = await repository.GetItemsAsync(GetDefaultAscendingListRequest());

            items.Should().BeInAscendingOrder(i => i.Id);
        }

        [TestMethod]
        public async Task Should_ReturnDescendingOrderedList_When_QueryDbSet()
        {
            var repository = GetDefaultRepository();

            var items = await repository.GetItemsAsync(GetDefaultDescendingListRequest());

            items.Should().BeInDescendingOrder(i => i.Id);
        }

        [TestMethod]
        public async Task Should_ReturnPaginatedAscendingOrderedList_When_QueryDbSet()
        {
            var repository = GetDefaultRepository();

            var paginationResult = await repository.GetPaginatedAsync(new PaginationRequest(0, 6, GetDefaultAscendingOrder(), string.Empty));

            paginationResult.Items.Should().BeInAscendingOrder(i => i.Id);
            paginationResult.Items.Should().HaveCount(6);
        }

        [TestMethod]
        public async Task Should_ReturnPaginatedDescendingOrderedList_When_QueryDbSet()
        {
            var repository = GetDefaultRepository();

            var paginationResult = await repository.GetPaginatedAsync(new PaginationRequest(0, 4, GetDefaultDescendingOrder(), string.Empty));

            paginationResult.Items.Should().BeInDescendingOrder(i => i.Id);
            paginationResult.Items.Should().HaveCount(4);
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
