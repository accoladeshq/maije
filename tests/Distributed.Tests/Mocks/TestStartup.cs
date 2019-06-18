using System.Collections.Generic;
using System.Reflection;
using Accolades.Maije.Distributed.WebApi;
using Microsoft.Extensions.DependencyInjection;

namespace Accolades.Maije.Distributed.Tests.Mocks
{
    internal class TestStartup : MaijeStartup
    {
        /// <summary>
        /// Configure the container
        /// </summary>
        /// <param name="services">The services</param>
        protected override void ConfigureContainer(IServiceCollection services)
        {
        }

        /// <summary>
        /// Gets the resource assemblies to scan
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<Assembly> GetResourceAssemblies()
        {
            yield return typeof(TestStartup).Assembly;
        }
    }
}
