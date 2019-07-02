using Accolades.Maije.Crosscutting.Enums;
using Accolades.Maije.Crosscutting.Exceptions;
using Accolades.Maije.Domain.Entities;
using Accolades.Maije.Infrastructure.Tests.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure.Tests
{
    public partial class DeactivatableRepositoryBaseTests
    {
        [TestMethod]
        public async Task Should_ReturnTestList_When_QueryDbSet()
        {
            var repository = GetDefaultRepository();

            var items = await repository.GetItemsAsync(GetDefaultAscendingListRequest());

            items.Should().HaveCount(Snapshot.ActivableTests.ActivateCount());
        }

        [TestMethod]
        public async Task Should_ReturnEntityWithId_When_IdExists()
        {
            var repository = GetDefaultRepository();

            var item = await repository.GetItemByIdAsync(Snapshot.ActivableTests.GetActivatedId());

            item.Should().NotBeNull();
        }

        [ExpectedException(typeof(InfrastructureException))]
        [TestMethod]
        public async Task Should_RaiseException_When_IdNotExists()
        {
            var repository = GetDefaultRepository();

            await repository.GetItemByIdAsync(Snapshot.ActivableTests.GetNonExistingId());
        }

        [ExpectedException(typeof(InfrastructureException))]
        [TestMethod]
        public async Task Should_RaiseException_When_IdExistsButNotActive()
        {
            var repository = GetDefaultRepository();

            await repository.GetItemByIdAsync(Snapshot.ActivableTests.GetDeactivatedId());
        }
    }
}
