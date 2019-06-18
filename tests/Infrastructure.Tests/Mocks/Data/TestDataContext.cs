using Accolades.Maije.Infrastructure.Data;
using Accolades.Maije.Infrastructure.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accolades.Maije.Infrastructure.Tests.Data
{
    internal class TestDbContext : MaijeDbContext
    {
        /// <summary>
        /// Initialize a new <see cref="TestDbContext"/>
        /// </summary>
        /// <param name="options">The database context options</param>
        public TestDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Initialize a new <see cref="TestDbContext"/>
        /// </summary>
        public TestDbContext()
        {

        }

        /// <summary>
        /// Gets the tests items
        /// </summary>
        public DbSet<Test> Tests { get; private set; }

        /// <summary>
        /// Gets the activate tests
        /// </summary>
        public DbSet<ActivableTest> ActivableTests { get; private set; }

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
