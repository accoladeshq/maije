using Accolades.Maije.Infrastructure.Exceptions;
using Accolades.Maije.Infrastructure.Tests.Extensions;
using Accolades.Maije.Infrastructure.Tests.Helpers;
using Accolades.Maije.Infrastructure.Tests.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure.Tests
{
    public partial class RepositoryBaseTests
    {
        [TestMethod]
        public async Task Should_RemoveEntityById_When_IdExists()
        {
            var repository = GetDefaultRepository();

            await repository.DeleteAsync(DatabaseHelper.MaxTestId);

            var deleted = DbContext.ChangeTracker.Entries().FirstById(DatabaseHelper.MaxTestId).State == EntityState.Deleted;

            var nbOfLines = DbContext.SaveChanges();

            nbOfLines.Should().Be(1);
            deleted.Should().BeTrue();
        }

        [ExpectedException(typeof(InfrastructureException))]
        [TestMethod]
        public async Task Should_NotRemoveEntityById_When_IdNotExists()
        {
            var repository = GetDefaultRepository();

            await repository.DeleteAsync(DatabaseHelper.MaxTestId + 1);

            var nbOfLines = DbContext.SaveChanges();

            nbOfLines.Should().Be(1);
        }

        [TestMethod]
        public async Task Should_DeactivateEntityById_When_IdExists()
        {
            var repository = GetActivateTestRepository();

            await repository.DeleteAsync(DatabaseHelper.MaxActivableTestId);

            var modified = DbContext.ChangeTracker.Entries().FirstById(DatabaseHelper.MaxActivableTestId).State == EntityState.Modified;

            var nbOfLines = DbContext.SaveChanges();

            modified.Should().BeTrue();
            nbOfLines.Should().Be(1);
        }

        private ActivateTestRepository GetActivateTestRepository()
        {
            return new ActivateTestRepository(DbContext);
        }
    }
}
