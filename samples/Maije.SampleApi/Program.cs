using Accolades.Maije.Distributed.WebApi;

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
        public static int Main(string[] args)
        {
            var maijeApp = new MaijeApp<Startup>(args);
            return maijeApp.Start();
        }
    }
}
