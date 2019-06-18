using Accolades.Maije.Distributed.Tests.Mocks;
using Accolades.Maije.Domain.Contracts;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Accolades.Maije.Distributed.Tests
{
    [TestClass]
    public class StartupTests
    {
        [TestMethod]
        public void Should_RegisterRepositories_When_Bootstrap()
        {
            var serviceProvider = GetDefaultServiceProvider();

            var repositoriesNames = typeof(StartupTests).Assembly.GetTypes().Where(type => type.Name.EndsWith("Repository")).Select(type => type.Name);
            var registerRepositoriesNames = serviceProvider.GetServices<IRepositoryBase>().Select(instance => instance.GetType().Name);

            repositoriesNames.Should().BeEquivalentTo(registerRepositoriesNames, o => o.WithoutStrictOrdering());
        }

        [TestMethod]
        public void Should_RegisterUnitOfWork_When_Bootstrap()
        {
            var serviceProvider = GetDefaultServiceProvider();

            var unitOfWork = serviceProvider.GetService<IUnitOfWork>();

            unitOfWork.Should().NotBeNull();
        }

        /// <summary>
        /// Gets the default service provider
        /// </summary>
        /// <returns></returns>
        private IServiceProvider GetDefaultServiceProvider()
        {
            var startup = new TestStartup();

            var serviceProvicer = startup.ConfigureServices(new ServiceCollection());

            return serviceProvicer;
        }
    }
}
