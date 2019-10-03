using Accolades.Maije.AppService;
using Accolades.Maije.Distributed.Tests.Mocks;
using Accolades.Maije.Distributed.Tests.Mocks.Dto;
using Accolades.Maije.Distributed.Tests.Mocks.Entities;
using Accolades.Maije.Distributed.Tests.Mocks.Repositories;
using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Infrastructure;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

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
            repositoryBase.Should().BeOfType<MaijeRepository<ValueTest, int>>();
        }

        [TestMethod]
        public void Should_RetrieveCustomRepository_When_BoostrapWithoutInterface()
        {
            var serviceProvider = GetDefaultServiceProvider();

            var customRepository = serviceProvider.GetService<IMaijeRepository<Category, Guid>>();

            customRepository.Should().NotBeNull();
            customRepository.Should().BeOfType<CategoryRepository>();
        }

        [TestMethod]
        public void Should_RetrieveCustomRepository_When_BoostrapWithInterface()
        {
            var serviceProvider = GetDefaultServiceProvider();

            var customRepository = serviceProvider.GetService<IPostRepository>();

            customRepository.Should().NotBeNull();
            customRepository.Should().BeOfType<PostRepository>();
        }

        [TestMethod]
        public void Should_NotRetrieveGenericRepository_When_BoostrapWithInterface()
        {
            var serviceProvider = GetDefaultServiceProvider();

            var customRepository = serviceProvider.GetService<IMaijeRepository<Post, string>>();

            customRepository.Should().BeNull();
        }

        [TestMethod]
        public void Should_RetrieveGenericDeactivatableRepository_When_Bootstrap()
        {
            var serviceProvider = GetDefaultServiceProvider();

            var repositoryBase = serviceProvider.GetService<IMaijeRepository<User, int>>();

            repositoryBase.Should().BeOfType<DeactivatableMaijeRepository<User, int>>();
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
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            var startup = new TestStartup(configuration);

            var serviceProvicer = startup.ConfigureServices(new ServiceCollection());

            return serviceProvicer;
        }
    }
}
