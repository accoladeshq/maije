using Accolades.Maije.Infrastructure.Exceptions;
using Accolades.Maije.Infrastructure.Tests.Commons;
using Accolades.Maije.Tests.Commons;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure.Tests
{
    [TestClass]
    public class RepositoryBaseTests : TestBase
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_RaiseException_When_DbContextIsNull()
        {
            new TestRepository(null);
        }

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

            var item = await repository.GetItemByIdAsync(1);

            item.Should().NotBeNull();
        }

        [ExpectedException(typeof(InfrastructureException))]
        [TestMethod]
        public async Task Should_RaiseException_When_IdNotExists()
        {
            var repository = GetDefaultRepository();

            await repository.GetItemByIdAsync(9999);
        }

        /// <summary>
        /// Get the default test repository
        /// </summary>
        /// <returns></returns>
        private TestRepository GetDefaultRepository()
        {
            return new TestRepository(DbContext);
        }
    }
}
