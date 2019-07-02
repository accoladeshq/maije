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
        public async Task<IActionResult> GetAll([FromQuery] PaginationRequestDto paginationRequestDto)
        {
            var dto = await ApplicationService.GetPaginatedAsync(paginationRequestDto);

            return Ok(dto);
        }

        protected override string GetDefaultOrderColumn()
        {
            return "id";
        }
    }
}
