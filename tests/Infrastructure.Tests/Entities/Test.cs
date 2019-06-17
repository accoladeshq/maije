using Accolades.Maije.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Accolades.Maije.Infrastructure.Tests.Entities
{
    public class Test : IIdentity<int>
    {
        /// <summary>
        /// Initialize a new <see cref="Test"/>
        /// </summary>
        /// <param name="id">The test identifier</param>
        public Test(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Initialize a new <see cref="Test" />
        /// </summary>
        public Test() : this(0)
        {

        }

        /// <summary>
        /// Gets the identifier
        /// </summary>
        [Key]
        public int Id { get; private set; }
    }
}
