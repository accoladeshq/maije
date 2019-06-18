using Accolades.Maije.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Accolades.Maije.Infrastructure.Tests.Entities
{
    public class Test : IIdentity<int>
    {
        /// <summary>
        /// Initialize a new <see cref="Test"/>
        /// </summary>
        /// <param name="id">The test identifier</param>
        private Test(int id)
        {
            Id = id;
            FixedCreationDate = DateTime.Parse("10/10/2019");
        }

        /// <summary>
        /// Initialize a new <see cref="Test"/>
        /// </summary>
        /// <param name="id">The test identifier</param>
        public Test(int id, string name) : this(id)
        {
            Name = name;
        }

        /// <summary>
        /// Initialize a new <see cref="Test" />
        /// </summary>
        public Test() : this(0, Guid.NewGuid().ToString())
        {

        }

        /// <summary>
        /// Gets the identifier
        /// </summary>
        [Key]
        public int Id { get; private set; }

        /// <summary>
        /// Gets the entity name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the test creation date
        /// </summary>
        public DateTime FixedCreationDate { get; private set; }
    }
}
