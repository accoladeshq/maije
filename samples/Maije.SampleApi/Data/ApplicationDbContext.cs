using Accolades.Maije.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accolades.Maije.SampleApi.Data
{
    public class ApplicationDbContext : MaijeDbContext
    {
        /// <summary>
        /// Initialize a new <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="options">The database context options</param>
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
