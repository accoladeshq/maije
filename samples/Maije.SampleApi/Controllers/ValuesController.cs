using Accolades.Maije.AppService;
using Accolades.Maije.AppService.Dto;
using Accolades.Maije.Distributed.WebApi;
using Accolades.Maije.SampleApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Accolades.Maije.SampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : MaijeController<ValueDto, int>
    {
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

        protected override string GetDefaultOrderColumn()
        {
            return "id";
        }
    }
}
