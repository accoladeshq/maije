using Accolades.Maije.Domain.Entities;
using Accolades.Maije.Domain.Entities.Commons;

namespace Accolades.Maije.Infrastructure.Tests.Entities
{
    public class ActivableTest : IIdentity<int>, IDeactivatable
    {
        /// <summary>
        /// Initialize a new <see cref="ActivableTest"/>
        /// </summary>
        /// <param name="id">The entity identifier</param>
        public ActivableTest(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Initialize a new <see cref="ActivableTest"/>
        /// </summary>
        public ActivableTest() : this(0)
        {
        }

        /// <summary>
        /// Gets or sets value indicating if the entity is active or not
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets the entity identifier
        /// </summary>
        public int Id { get; private set; }
    }
}
