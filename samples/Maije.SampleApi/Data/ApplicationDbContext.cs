using Accolades.Maije.Infrastructure.Data;
using Accolades.Maije.SampleApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accolades.Maije.SampleApi.Data
{
    internal class ApplicationDbContext : MaijeDbContext
    {
        /// <summary>
        /// Initialize a new <see cref="ApplicationDbContext"/>
        /// </summary>
        /// <param name="options">The database context options</param>
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Value> Values { get; private set; }

        public DbSet<User> Users { get; private set; }
    }
}
