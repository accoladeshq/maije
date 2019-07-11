using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Accolades.Maije.SampleApi
{
    /// <summary>
    /// The program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The application main entry points
        /// </summary>
        /// <param name="args">The application arguments</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create a default <see cref="IWebHostBuilder"/>
        /// </summary>
        /// <param name="args">The application arguments</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();
    }
}
