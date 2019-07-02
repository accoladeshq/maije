using Accolades.Maije.Crosscutting.Enums;
using Accolades.Maije.Domain.Entities;
using Accolades.Maije.Infrastructure.Data;
using Accolades.Maije.Infrastructure.Tests.Data;
using Accolades.Maije.Infrastructure.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Accolades.Maije.Infrastructure.Tests
{
    public class TestBase
    {
        /// <summary>
        /// Gets the data context
        /// </summary>
        protected MaijeDbContext DbContext { get; private set; }

        /// <summary>
        /// Gets the data context snapshot
        /// </summary>
        protected DataContextSnapshot Snapshot { get; private set; }

        /// <summary>
        /// Initializes the test.
        /// </summary>
        /// <param name="context">The context.</param>
        [TestInitialize]
        public void TestInitialize()
        {
            // We initialize the database at each test
            // The test must not be dependant of a test order etc...
            var testDbContext = new TestDbContext();
            var snapshot = testDbContext.Initialize();

            DbContext = testDbContext;
            Snapshot = snapshot;

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

        /// <summary>
        /// Gets a default ascending order
        /// </summary>
        /// <returns></returns>
        protected Order GetDefaultAscendingOrder()
        {
            return new Order(OrderType.Asc, "id");
        }

        /// <summary>
        /// Gets a default descending order
        /// </summary>
        /// <returns></returns>
        protected Order GetDefaultDescendingOrder()
        {
            return new Order(OrderType.Desc, "id");
        }

        /// <summary>
        /// Gets a default ascending order
        /// </summary>
        /// <returns></returns>
        protected ListRequest GetDefaultAscendingListRequest()
        {
            return new ListRequest(GetDefaultAscendingOrder(), string.Empty);
        }

        /// <summary>
        /// Gets a default descending order
        /// </summary>
        /// <returns></returns>
        protected ListRequest GetDefaultDescendingListRequest()
        {
            return new ListRequest(GetDefaultDescendingOrder(), string.Empty);
        }
    }
}
