using Accolades.Maije.Domain.Entities;

namespace Accolades.Maije.SampleApi.Entities
{
    internal class Value : IIdentity<int>
    {
        /// <summary>
        /// Initialize a new <see cref="Value"/>
        /// </summary>
        /// <param name="id"></param>
        public Value(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Initialize a new <see cref="Value"/>
        /// </summary>
        public Value() : this(0)
        {

        }

        /// <summary>
        /// Gets the value identifier
        /// </summary>
        public int Id { get; private set; }
    }
}
