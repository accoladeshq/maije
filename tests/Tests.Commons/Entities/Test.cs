using Accolades.Maije.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accolades.Maije.Tests.Commons.Entities
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
        public Test() : this(-1)
        {

        }

        /// <summary>
        /// Gets the identifier
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; private set; }
    }
}
