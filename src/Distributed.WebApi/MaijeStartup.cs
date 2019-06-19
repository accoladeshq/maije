using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Infrastructure;
using Accolades.Maije.Infrastructure.Data;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Accolades.Maije.Distributed.WebApi
{
    public abstract class MaijeStartup
    {
        /// <summary>
        /// Configure services available in the application
        /// </summary>
        /// <param name="services">The service collection</param>
        /// <returns>The service provier</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            var scanAssemblies = GetResourceAssemblies().ToArray();

            builder.RegisterGeneric(typeof(RepositoryBase<,>)).As(typeof(IRepositoryBase<,>));

            builder.RegisterAssemblyTypes(scanAssemblies)
                .Where(t => t.Name.EndsWith("Repository") || typeof(IMaijeDbContext).IsAssignableFrom(t))
                .AsImplementedInterfaces();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            ConfigureContainer(services);

            // Note that Populate is basically a foreach to add things
            // into Autofac that are in the collection. If you register
            // things in Autofac BEFORE Populate then the stuff in the
            // ServiceCollection can override those things; if you register
            // AFTER Populate those registrations can override things
            // in the ServiceCollection..
            builder.Populate(services);

            return new AutofacServiceProvider(builder.Build());
        }

        /// <summary>
        /// Configure the application container
        /// </summary>
        /// <param name="services"></param>
        protected abstract void ConfigureContainer(IServiceCollection services);

        /// <summary>
        /// Gets the resource assemblies to register app resources
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<Assembly> GetResourceAssemblies()
        {
            yield return Assembly.GetEntryAssembly();
        }
    }
}
