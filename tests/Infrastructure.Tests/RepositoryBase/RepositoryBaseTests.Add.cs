using Accolades.Maije.Crosscutting.Exceptions;
using Accolades.Maije.Infrastructure.Tests.Entities;
using Accolades.Maije.Infrastructure.Tests.Extensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure.Tests
{
    [TestClass]
    public partial class RepositoryBaseTests
    {
        [TestMethod]
        public async Task Should_AddEntity_When_EntityNotExists()
        {
            var repository = GetDefaultRepository();

            var newTest = new Test();
            await repository.AddAsync(newTest);

            var added = DbContext.ChangeTracker.Entries().First(e => e.Entity.Equals(newTest)).State == EntityState.Added;

            var nbOfEntityAdd = DbContext.SaveChanges();            

            nbOfEntityAdd.Should().Be(1);
            added.Should().BeTrue();
        }

        [ExpectedException(typeof(InfrastructureException))]
        [TestMethod]
        public async Task Should_NotAddEntityAsync_When_EntityExists()
        {
            var repository = GetDefaultRepository();

            var newTest = GetTestWithExistingId();
            await repository.AddAsync(newTest);

            await DbContext.SaveChangesAsync();
        }

        [ExpectedException(typeof(InfrastructureException))]
        [TestMethod]
        public async Task Should_NotAddEntity_When_EntityExists()
        {
            var repository = GetDefaultRepository();

            var newTest = GetTestWithExistingId();
            await repository.AddAsync(newTest);

            DbContext.SaveChanges();
        }

        /// <summary>
        /// Gets a test with an existing identifier
        /// </summary>
        /// <returns></returns>
        private Test GetTestWithExistingId()
        {
            return new Test(Snapshot.Tests.GetExistingId(), "Random Name");
        }
    }
}
