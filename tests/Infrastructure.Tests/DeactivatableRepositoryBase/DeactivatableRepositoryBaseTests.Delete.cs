using Accolades.Maije.Infrastructure.Tests.Extensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure.Tests
{
    public partial class DeactivatableRepositoryBaseTests
    {
        [TestMethod]
        public async Task Should_DeactivateEntityById_When_IdExists()
        {
            var repository = GetDefaultRepository();

            var existingId = Snapshot.ActivableTests.GetActivatedId();

            await repository.DeleteAsync(existingId);

            var modified = DbContext.ChangeTracker.Entries().FirstById(existingId).State == EntityState.Modified;

            var nbOfLines = DbContext.SaveChanges();

            modified.Should().BeTrue();
            nbOfLines.Should().Be(1);
        }
    }
}
