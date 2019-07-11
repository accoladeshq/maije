using Accolades.Maije.Domain.Entities;

namespace Accolades.Maije.SampleApi.Entities
{
    internal class User : IIdentity<int>
    {
        public int Id { get; set; }
    }
}
