using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Infrastructure.Data;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
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

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.GetEntryAssembly())
                .Where(t => typeof(IMaijeDbContext).IsAssignableFrom(t))
                .AsImplementedInterfaces();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            ConfigureContainer(services);

            // Register dependencies, populate the services from
            // the collection, and build the container.
            //
            // Note that Populate is basically a foreach to add things
            // into Autofac that are in the collection. If you register
            // things in Autofac BEFORE Populate then the stuff in the
            // ServiceCollection can override those things; if you register
            // AFTER Populate those registrations can override things
            // in the ServiceCollection..
            builder.Populate(services);

            // Create the IServiceProvider based on the container.
            return new AutofacServiceProvider(builder.Build());
        }

        /// <summary>
        /// Configure the application container
        /// </summary>
        /// <param name="services"></param>
        protected abstract void ConfigureContainer(IServiceCollection services);
    }
}
