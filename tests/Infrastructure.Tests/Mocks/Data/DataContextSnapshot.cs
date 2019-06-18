using Accolades.Maije.Infrastructure.Tests.Entities;
using System.Collections.Generic;

namespace Accolades.Maije.Infrastructure.Tests.Data
{
    public class DataContextSnapshot
    {
        /// <summary>
        /// Initialize a new <see cref="DataContextSnapshot"/>
        /// </summary>
        public DataContextSnapshot()
        {
            Tests = new List<Test>();
            ActivableTests = new List<ActivableTest>();
        }

        /// <summary>
        /// Gets the tests
        /// </summary>
        public IList<Test> Tests { get; private set; }

        /// <summary>
        /// Gets the activatable tests
        /// </summary>
        public IList<ActivableTest> ActivableTests { get; private set; }
    }
}
