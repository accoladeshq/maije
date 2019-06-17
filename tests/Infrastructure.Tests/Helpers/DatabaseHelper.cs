using Accolades.Maije.Infrastructure.Tests.Data;
using Accolades.Maije.Infrastructure.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Accolades.Maije.Infrastructure.Tests.Helpers
{
    public class DatabaseHelper
    {
        private readonly static List<Test> Tests = new List<Test>();

        /// <summary>
        /// Gets a new <see cref="DbContext"/>
        /// </summary>
        /// <returns></returns>
        public static DbContext GetDatabaseContext()
        {
            using(var c = new TestDbContext())
            {
                for (int i = 1; i <= 10; i++)
                {
                    var newTest = new Test();

                    c.Tests.Add(newTest);
                    Tests.Add(newTest);
                }
                
                c.SaveChanges();
            }

            return new TestDbContext();
        }

        public static int MaxTestId
        {
            get { return Tests.Select(t => t.Id).Max(); }
        }
    }
}
