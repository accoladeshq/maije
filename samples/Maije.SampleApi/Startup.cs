using Accolades.Maije.Distributed.WebApi;
using Accolades.Maije.SampleApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accolades.Maije.SampleApi
{
    internal class Startup : MaijeStartup
    {
        public Startup(IConfiguration configuration): base(configuration)
        {

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
