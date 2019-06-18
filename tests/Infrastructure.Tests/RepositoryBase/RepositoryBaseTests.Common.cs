using Accolades.Maije.Infrastructure.Tests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Accolades.Maije.Infrastructure.Tests
{
    public partial class RepositoryBaseTests : TestBase
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_RaiseException_When_DbContextIsNull()
        {
            new TestRepository(null);
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
