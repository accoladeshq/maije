using Accolades.Maije.AppService;
using Accolades.Maije.AppService.Dto;
using Accolades.Maije.Crosscutting.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Accolades.Maije.Distributed.WebApi
{
    public abstract class MaijeController : ControllerBase
    {
        /// <summary>
        /// The logger service
        /// </summary>
        protected readonly ILogger Logger;

        /// <summary>
        /// Initialize a new <see cref="MaijeController"/>
        /// </summary>
        /// <param name="logger">The service who manage logs</param>
        public MaijeController(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Handle exception
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        protected virtual IActionResult ManageException(Exception exception)
        {
            ErrorDto error;

            switch (exception)
            {
                case BusinessException _:
                    Logger.LogError(400, exception.Message);
                    error = new ErrorDto { Message = "invalid_request", Description = exception.Message };
                    return BadRequest(error);
            }

            Logger.LogError(520, exception.Message);
            return StatusCode(520);
        }
    }

    public abstract class MaijeController<TDto, TIdentifier> : MaijeController<IMaijeAppService<TDto, TIdentifier>,TDto, TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
        where TDto : IIdentityDto<TIdentifier>
    {
        /// <summary>
        /// Initialize a new <see cref="MaijeController"/>
        /// </summary>
        /// <param name="appService">The application service</param>
        /// <param name="logger">The logger</param>
        public MaijeController(IMaijeAppService<TDto, TIdentifier> appService, ILogger logger) : base(appService, logger)
        {
        }
    }

    public abstract class MaijeController<TAppService, TDto, TIdentifier> : MaijeController
        where TAppService : IMaijeAppService<TDto, TIdentifier>
        where TDto : IIdentityDto<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// The application service corresponding to this controller
        /// </summary>
        protected readonly TAppService ApplicationService;

        /// <summary>
        /// Initialize <see cref="CityControllerBase{TAppService, TDto}"/>
        /// </summary>
        /// <param name="appService">The main application service used by this controller</param>
        /// <param name="logger">The logger service</param>
        public MaijeController(TAppService appService, ILogger logger) : base(logger)
        {
            ApplicationService = appService;
        }

        /// <summary>
        /// Gets paginated result
        /// </summary>
        /// <param name="requestPaginationInfo">The pagination infos</param>
        /// <returns></returns>
        protected virtual async Task<IActionResult> GetPaginatedAsyncInternal([FromQuery] PaginationRequestDto requestPaginationInfo)
        {
            try
            {
                if (string.IsNullOrEmpty(requestPaginationInfo.OrderColumnName))
                    requestPaginationInfo.OrderColumnName = GetDefaultOrderColumn();

                var result = await ApplicationService.GetPaginatedAsync(requestPaginationInfo);

                Response.Headers.Add("Link", string.Join(",", result.Links.Select(l => $"<{l.Href}>; rel={l.Rel};")));
                Response.Headers.Add("X-Pagination-Total-Count", result.Pagination.Total.ToString());
                Response.Headers.Add("X-Pagination-Offset", result.Pagination.Offset.ToString());
                Response.Headers.Add("X-Pagination-Limit", result.Pagination.Limit.ToString());

                return Ok(result.Items);
            }
            catch (Exception e)
            {
                return ManageException(e);
            }
        }

        /// <summary>
        /// Gets items
        /// </summary>
        /// <param name="listRequest">The list request</param>
        /// <returns></returns>
        protected virtual async Task<IActionResult> GetItemsAsyncInternal(ListRequestDto listRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(listRequest.OrderColumnName))
                    listRequest.OrderColumnName = GetDefaultOrderColumn();

                var result = await ApplicationService.GetItemsAsync(listRequest);

                return Ok(result);
            }
            catch (Exception e)
            {
                return ManageException(e);
            }
        }

        /// <summary>
        /// Gets an entity by it's identifier
        /// </summary>
        /// <param name="identifier">The entity identifier</param>
        /// <returns></returns>
        protected virtual async Task<IActionResult> GetByIdAsyncInternal(TIdentifier identifier)
        {
            try
            {
                var result = await ApplicationService.GetByIdAsync(identifier);

                return Ok(result);
            }
            catch (Exception e)
            {
                return ManageException(e);
            }
        }

        /// <summary>
        /// Update an entity by it's identifier
        /// </summary>
        /// <param name="updatableDto">The entity to update</param>
        /// <returns>The updated entity</returns>
        protected virtual async Task<IActionResult> UpdateByIdAsyncInternal(IUpdatableDto<TIdentifier> updatableDto)
        {
            try
            {
                var updateDto = await ApplicationService.UpdateAsync(updatableDto);

                return Ok(updateDto);
            }
            catch (Exception e)
            {
                return ManageException(e);
            }
        }

        /// <summary>
        /// Create an entity
        /// </summary>
        /// <param name="creatableDto">The entity to create</param>
        /// <returns>The create entity</returns>
        protected virtual async Task<IActionResult> CreateAsyncInternal(ICreatableDto creatableDto)
        {
            try
            {
                var newIdentifier = await ApplicationService.CreateAsync(creatableDto);

                return CreatedAtAction("GetByIdAsync", new { id = newIdentifier }, new { id = newIdentifier });
            }
            catch (Exception e)
            {
                return ManageException(e);
            }
        }

        /// <summary>
        /// Delete an entity asynchronously
        /// </summary>
        /// <param name="id">The identifier of the entity to delete</param>
        /// <returns></returns>
        protected virtual async Task<IActionResult> DeleteAsyncInternal(TIdentifier id)
        {
            try
            {
                await ApplicationService.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception e)
            {
                return ManageException(e);
            }
        }

        /// <summary>
        /// Gets the default order dto if missing
        /// </summary>
        /// <returns></returns>
        protected abstract string GetDefaultOrderColumn();
    }
}
