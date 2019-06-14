using Accolades.Maije.Tests.Commons.Data;
using Accolades.Maije.Tests.Commons.Entities;
using Microsoft.EntityFrameworkCore;

namespace Accolades.Maije.Tests.Commons.Helpers
{
    internal class DatabaseHelper
    {
        /// <summary>
        /// Gets a new <see cref="DbContext"/>
        /// </summary>
        /// <returns></returns>
        public static DbContext GetDatabaseContext()
        {
            using(var c = new TestDbContext())
            {
                for (int i = 0; i < 10; i++)
                {
                    c.Tests.Add(new Test(i));
                }
                
                c.SaveChanges();
            }

            return new TestDbContext();
        }
    }
}
