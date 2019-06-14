using Accolades.Maije.Tests.Commons.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Accolades.Maije.Tests.Commons
{
    public class TestBase
    {
        /// <summary>
        /// Gets the data context
        /// </summary>
        protected DbContext DbContext { get; private set; }

        /// <summary>
        /// Initializes the test.
        /// </summary>
        /// <param name="context">The context.</param>
        [TestInitialize]
        public void TestInitialize()
        {
            // We initialize the database at each test
            // The test must not be dependant of a test order etc...
            DbContext = DatabaseHelper.GetDatabaseContext();
        }

        /// <summary>
        /// Cleanups the test.
        /// </summary>
        [TestCleanup]
        public void TestCleanup()
        {
            if (DbContext != null)
            {
                DbContext.Database.EnsureDeleted();
                DbContext.Dispose();
            }
        }
    }
}
