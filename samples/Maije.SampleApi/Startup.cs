using Accolades.Maije.Distributed.WebApi;
using Accolades.Maije.SampleApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Accolades.Maije.SampleApi
{
    public class Startup : MaijeStartup
    {
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
