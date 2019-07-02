using Accolades.Maije.AppService.Dto;
using Accolades.Maije.Crosscutting.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accolades.Maije.AppService
{
    public interface IMaijeAppService<TDto, TIdentifier> where TDto : IIdentityDto<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// Gets the page asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<PaginationResultDto<TDto>> GetPaginatedAsync(PaginationRequestDto paginationRequest);

        /// <summary>
        /// Gets items
        /// </summary>
        /// <param name="listRequest">The list request</param>
        /// <returns></returns>
        Task<IEnumerable<TDto>> GetItemsAsync(ListRequestDto listRequest);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns></returns>
        Task<TDto> GetByIdAsync(TIdentifier identifier);

        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="createDto">The object to create.</param>
        /// <returns></returns>
        Task<TIdentifier> CreateAsync(ICreatableDto createDto);

        /// <summary>
        /// Create a new resource
        /// </summary>
        /// <param name="createDto">The object to create.</param>
        /// <param name="additionalMapping">The additional property to add</param>
        /// <returns></returns>
        Task<TIdentifier> CreateAsync(ICreatableDto createDto, Dictionary<string, string> additionalMapping);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="objectToUpdate">The object to update.</param>
        /// <returns></returns>
        Task<TDto> UpdateAsync(IUpdatableDto<TIdentifier> objectToUpdate);

        /// <summary>
        /// Delete an entity asynchronously
        /// </summary>
        /// <param name="identifierOfEntityToDelete">The identifier of the entity to delete</param>
        /// <returns></returns>
        Task DeleteAsync(TIdentifier identifierOfEntityToDelete);
    }
}
