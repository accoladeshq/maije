using Accolades.Maije.Tests.Commons.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accolades.Maije.Tests.Commons.Data
{
    internal class TestDbContext : DbContext
    {
        /// <summary>
        /// Gets the tests items
        /// </summary>
        public DbSet<Test> Tests { get; private set; }

        /// <summary>
        /// Occurs during database context configuration
        /// </summary>
        /// <param name="optionsBuilder">The database context options builder</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (optionsBuilder.IsConfigured) return;
            
            optionsBuilder.UseInMemoryDatabase("MaijeTests");
        }
    }
}
