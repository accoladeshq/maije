using Accolades.Maije.AppService;
using Accolades.Maije.Distributed.Tests.Mocks;
using Accolades.Maije.Distributed.Tests.Mocks.Dto;
using Accolades.Maije.Distributed.Tests.Mocks.Entities;
using Accolades.Maije.Domain.Contracts;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Accolades.Maije.Distributed.Tests
{
    [TestClass]
    public class StartupTests
    {
        [TestMethod]
        public void Should_RetrieveGenericRepository_When_Bootstrap()
        {
            var serviceProvider = GetDefaultServiceProvider();

            var repositoryBase = serviceProvider.GetService<IMaijeRepository<ValueTest, int>>();

            repositoryBase.Should().NotBeNull();
        }

        [TestMethod]
        public void Should_RetrieveGenericAppService_When_Bootstrap()
        {
            var servicesProvider = GetDefaultServiceProvider();

            var appService = servicesProvider.GetService<IMaijeAppService<ValueTestDto, int>>();

            appService.Should().NotBeNull();
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
