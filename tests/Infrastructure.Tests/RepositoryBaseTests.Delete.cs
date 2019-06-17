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
        public async Task Should_RemoveEntityById_When_IdExists()
        {
            var repository = GetDefaultRepository();

            await repository.DeleteAsync(DatabaseHelper.MaxTestId);

            var nbOfLines = DbContext.SaveChanges();

            nbOfLines.Should().Be(1);
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
    }
}
