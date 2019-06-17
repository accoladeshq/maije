using Accolades.Maije.Infrastructure.Exceptions;
using Accolades.Maije.Infrastructure.Tests.Entities;
using Accolades.Maije.Infrastructure.Tests.Helpers;
using Accolades.Maije.Infrastructure.Tests.Repositories;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

            var nbOfEntityAdd = DbContext.SaveChanges();

            nbOfEntityAdd.Should().Be(1);
        }

        [ExpectedException(typeof(InfrastructureException))]
        [TestMethod]
        public async Task Should_NotAddEntityAsync_When_EntityExists()
        {
            var repository = GetDefaultRepository();

            var newTest = new Test(DatabaseHelper.MaxTestId);
            await repository.AddAsync(newTest);

            await DbContext.SaveChangesAsync();
        }

        [ExpectedException(typeof(InfrastructureException))]
        [TestMethod]
        public async Task Should_NotAddEntity_When_EntityExists()
        {
            var repository = GetDefaultRepository();

            var newTest = new Test(DatabaseHelper.MaxTestId);
            await repository.AddAsync(newTest);

            DbContext.SaveChanges();
        }
    }
}
