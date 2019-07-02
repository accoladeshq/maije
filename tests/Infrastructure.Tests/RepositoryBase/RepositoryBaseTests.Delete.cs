using Accolades.Maije.Crosscutting.Exceptions;
using Accolades.Maije.Infrastructure.Tests.Extensions;
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
            var existingId = Snapshot.Tests.GetExistingId();

            await repository.DeleteAsync(existingId);

            var deleted = DbContext.ChangeTracker.Entries().FirstById(existingId).State == EntityState.Deleted;

            var nbOfLines = DbContext.SaveChanges();

            nbOfLines.Should().Be(1);
            deleted.Should().BeTrue();
        }

        [ExpectedException(typeof(InfrastructureException))]
        [TestMethod]
        public async Task Should_NotRemoveEntityById_When_IdNotExists()
        {
            var repository = GetDefaultRepository();

            await repository.DeleteAsync(Snapshot.Tests.GetNonExistingId());

            DbContext.SaveChanges();
        }
    }
}
