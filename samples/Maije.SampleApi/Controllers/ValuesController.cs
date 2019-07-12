using Accolades.Maije.AppService;
using Accolades.Maije.AppService.Dto;
using Accolades.Maije.Distributed.WebApi;
using Accolades.Maije.SampleApi.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Accolades.Maije.SampleApi.Controllers
{
    /// <summary>
    /// Manage values
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : MaijeController<ValueDto, int>
    {
        /// <summary>
        /// Initialize a new <see cref="ValuesController"/>
        /// </summary>
        /// <param name="appService">The application service</param>
        /// <param name="logger">The logger service</param>
        public ValuesController(IMaijeAppService<ValueDto, int> appService, ILogger<ValuesController> logger) : base(appService, logger)
        {
        }

        /// <summary>
        /// Gets all values
        /// </summary>
        /// <param name="paginationRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<IActionResult> GetAll([FromQuery] PaginationRequestDto paginationRequestDto)
        {
            return GetPaginatedAsyncInternal(paginationRequestDto);
        }

        /// <summary>
        /// Gets the default column order name
        /// </summary>
        /// <returns></returns>
        protected override string GetDefaultOrderColumn()
        {
            return "id";
        }
    }
}
