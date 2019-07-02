using Accolades.Maije.AppService.Dto;
using Accolades.Maije.AppService.Extensions;
using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accolades.Maije.AppService
{
    public abstract class MaijeAppService : IDisposable
    {
        /// <summary>
        /// The unit of work
        /// </summary>
        protected readonly IUnitOfWork UnitOfWork;

        /// <summary>
        /// The mapper
        /// </summary>
        protected readonly IMapper Mapper;

        /// <summary>
        /// The disposed value used to detect redundant calls
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaijeAppService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public MaijeAppService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    UnitOfWork.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }

    public class MaijeAppServiceBase<TDto, TEntity, TIdentifier, TRepository> : MaijeAppService, IMaijeAppService<TDto, TIdentifier>
        where TEntity : class, IIdentity<TIdentifier>
        where TDto : IIdentityDto<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
        where TRepository: IMaijeRepository<TEntity, TIdentifier>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppServiceBase{T}"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="mapper"></param>
        public MaijeAppServiceBase(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns></returns>
        public virtual async Task<TDto> GetByIdAsync(TIdentifier identifier)
        {
            var repository = UnitOfWork.GetRepository<TRepository>();

            var entity = await repository.GetItemByIdAsync(identifier);

            var dto = Mapper.Map<TDto>(entity);

            return dto;
        }


        /// <summary>
        /// Gets the page asynchronous.
        /// </summary>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns></returns>
        public async virtual Task<PaginationResultDto<TDto>> GetPaginatedAsync(PaginationRequestDto paginationRequest)
        {
            var paginationRequestEntity = Mapper.Map<PaginationRequest>(paginationRequest);

            var repository = UnitOfWork.GetRepository<TRepository>();

            var entities = await repository.GetPaginatedAsync(paginationRequestEntity);

            var dto = Mapper.Map<PaginationResultDto<TDto>>(entities);

            return dto;
        }

        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="createDto">The object to create.</param>
        /// <returns></returns>
        public virtual Task<TIdentifier> CreateAsync(ICreatableDto createDto)
        {
            return CreateAsync(createDto, null);
        }

        /// <summary>
        /// Create a new entity
        /// </summary>
        /// <param name="createDto">The entity to create</param>
        /// <param name="additionalMapping">The entity mapping</param>
        /// <returns></returns>
        public virtual async Task<TIdentifier> CreateAsync(ICreatableDto createDto, Dictionary<string, string> additionalMapping)
        {
            TEntity entity;

            if (additionalMapping == null)
                entity = Mapper.Map<TEntity>(createDto);
            else
                entity = Mapper.Map<TEntity>(createDto, additionalMapping.ToMappingOperationOptionsAction());

            var repository = UnitOfWork.GetRepository<TRepository>() as IMaijeRepository<TEntity, TIdentifier>;

            var createdEntityIdentifier = await repository.AddAsync(entity);

            await UnitOfWork.CommitAsync();

            return createdEntityIdentifier;
        }



        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="itemIdentifier">The item identifier</param>
        /// <param name="objectToUpdate">The object to update.</param>
        /// <returns></returns>
        public virtual async Task<TDto> UpdateAsync(IUpdatableDto<TIdentifier> objectToUpdate)
        {
            var repository = UnitOfWork.GetRepository<TRepository>() as IMaijeRepository<TEntity, TIdentifier>;

            var entity = await repository.GetItemByIdAsync(objectToUpdate.Id);

            Mapper.Map(objectToUpdate, entity);

            var updatedEntity = await repository.UpdateAsync(entity);

            await UnitOfWork.CommitAsync();

            return Mapper.Map<TDto>(updatedEntity);
        }

        /// <summary>
        /// Delete an entity asynchronously
        /// </summary>
        /// <param name="identifier">The identifier of the entity to delete</param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TIdentifier identifier)
        {
            var repository = UnitOfWork.GetRepository<TRepository>() as IMaijeRepository<TEntity, TIdentifier>;

            await repository.DeleteAsync(identifier);

            await UnitOfWork.CommitAsync();
        }
    }
}
