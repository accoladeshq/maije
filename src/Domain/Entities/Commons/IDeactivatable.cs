namespace Accolades.Maije.Domain.Entities
{
    public interface IDeactivatable
    {
        /// <summary>
        /// Gets value indicating if the entity is active or not
        /// </summary>
        bool IsActive { get; set; }
    }
}
