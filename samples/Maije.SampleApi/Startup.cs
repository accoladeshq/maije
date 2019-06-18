using Accolades.Maije.Distributed.WebApi;
using Accolades.Maije.SampleApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Accolades.Maije.SampleApi
{
    public class Startup : MaijeStartup
    {
        /// <summary>
        /// Configure the application pipeline
        /// </summary>
        /// <param name="app"></param>
        public void Configure(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        /// <summary>
        /// Configure the application container
        /// </summary>
        /// <param name="services"></param>
        protected override void ConfigureContainer(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(o =>
            {
                o.UseInMemoryDatabase("Maije-Sample");
            });
        }
    }
}
