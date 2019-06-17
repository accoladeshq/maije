using Accolades.Maije.Infrastructure.Exceptions;
using Accolades.Maije.Infrastructure.Tests.Helpers;
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

            items.Should().HaveCountGreaterThan(0);
        }

        [TestMethod]
        public async Task Should_ReturnEntityWithId_When_IdExists()
        {
            var repository = GetDefaultRepository();

            var item = await repository.GetItemByIdAsync(DatabaseHelper.MaxTestId);

            item.Should().NotBeNull();
        }

        [ExpectedException(typeof(InfrastructureException))]
        [TestMethod]
        public async Task Should_RaiseException_When_IdNotExists()
        {
            var repository = GetDefaultRepository();

            await repository.GetItemByIdAsync(DatabaseHelper.MaxTestId + 1);
        }
    }
}
