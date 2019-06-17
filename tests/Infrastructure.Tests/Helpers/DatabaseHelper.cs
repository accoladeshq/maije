using Accolades.Maije.Infrastructure.Tests.Data;
using Accolades.Maije.Infrastructure.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accolades.Maije.Infrastructure.Tests.Helpers
{
    public class DatabaseHelper
    {
        private readonly static List<Test> Tests = new List<Test>();

        private readonly static List<ActivableTest> ActivableTests = new List<ActivableTest>();

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

                for (int i = 0; i < 10; i++)
                {
                    var newActivableTest = new ActivableTest();

                    c.ActivableTests.Add(newActivableTest);
                    ActivableTests.Add(newActivableTest);
                }
                
                c.SaveChanges();
            }

            return new TestDbContext();
        }

        public static int MaxTestId
        {
            get { return Tests.Select(t => t.Id).Max(); }
        }

        public static Guid MaxActivableTestId
        {
            get { return ActivableTests.Last().Id; }
        }
    }
}
