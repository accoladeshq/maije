using Accolades.Maije.Infrastructure.Tests.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure.Tests
{
    public partial class RepositoryBaseTests
    {
        [TestMethod]
        public async Task Should_UpdateEntity_When_PropertiesMarkAsModified()
        {
            var repository = GetDefaultRepository();

            var existingTest = GetTestWithExistingId();
            existingTest.Name = "Updated Entity";

            await repository.UpdateAsync(existingTest);

            var entityFrameworkModifiedProperties = DbContext.ChangeTracker
                .Entries<Test>()
                .Where(e => e.State == EntityState.Modified)
                .Single()
                .Properties.Where(p => p.Metadata.Name != "Id" && p.IsModified)
                .Select(p => p.Metadata.Name);

            var entityProperties = typeof(Test)
                .GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.Instance)
                .Where(p => p.Name != "Id")
                .Select(p => p.Name);

            entityProperties.Should().BeEquivalentTo(entityFrameworkModifiedProperties, o => o.WithoutStrictOrdering());

            var nbOfLines = DbContext.SaveChanges();

            nbOfLines.Should().Be(1);
        }
    }
}
