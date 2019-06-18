﻿using Accolades.Maije.Infrastructure.Tests.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Accolades.Maije.Infrastructure.Tests
{
    [TestClass]
    public partial class DeactivatableRepositoryBaseTests : TestBase
    {
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Should_RaiseException_When_DbContextIsNull()
        {
            new ActivateTestRepository(null);
        }

        /// <summary>
        /// Get the default test repository
        /// </summary>
        /// <returns></returns>
        private ActivateTestRepository GetDefaultRepository()
        {
            return new ActivateTestRepository(DbContext);
        }
    }
}
