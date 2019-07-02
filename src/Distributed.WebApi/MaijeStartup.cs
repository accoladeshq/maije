using Accolades.Maije.AppService;
using Accolades.Maije.Distributed.WebApi.Extensions;
using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Domain.Services;
using Accolades.Maije.Infrastructure.Data;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
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
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                });

            var builder = new ContainerBuilder();

            var baseAssemblies = new List<Assembly> { typeof(MaijeAppService).Assembly };
            var scanAssemblies = GetResourceAssemblies().Union(baseAssemblies).ToArray();

            services.AddAutoMapper(scanAssemblies);

            services.AddMaijeRouteContext();
            services.AddMaijeGenerics(scanAssemblies);

            services.AddScoped<IPaginationDomainService, PaginationDomainService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            ConfigureContainer(services);

            // override from services
            builder.Populate(services);

            return new AutofacServiceProvider(builder.Build());
        }

        /// <summary>
        /// Configure the application pipeline
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();
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
