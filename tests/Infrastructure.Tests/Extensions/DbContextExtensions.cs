using Accolades.Maije.Infrastructure.Tests.Data;
using Accolades.Maije.Infrastructure.Tests.Entities;

namespace Accolades.Maije.Infrastructure.Tests.Extensions
{
    internal static class DbContextExtensions
    {
#pragma warning disable IDE0060 // Remove unused parameter
        public static DataContextSnapshot Initialize(this TestDbContext unused)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            var snapshot = new DataContextSnapshot();

            using(var dbContext = new TestDbContext())
            {
                for (int i = 1; i <= 10; i++)
                {
                    var newTest = new Test();

                    dbContext.Tests.Add(newTest);
                    snapshot.Tests.Add(newTest);
                }

                for (int i = 0; i < 10; i++)
                {
                    var newActivableTest = new ActivableTest
                    {
                        IsActive = i % 2 == 0
                    };

                    dbContext.ActivableTests.Add(newActivableTest);
                    snapshot.ActivableTests.Add(newActivableTest);
                }

                dbContext.SaveChanges();
            }

            return snapshot;
        }
    }
}
