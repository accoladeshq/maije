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
    public class UsersController : MaijeController<UserDto, int>
    {
        public UsersController(IMaijeAppService<UserDto, int> appService, ILogger<UsersController> logger) : base(appService, logger)
        {
        }

        /// <summary>
        /// Gets all values
        /// </summary>
        /// <param name="listRequestDto"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<IActionResult> GetAll([FromQuery] ListRequestDto listRequestDto)
        {
            return GetItemsAsyncInternal(listRequestDto);
        }

        protected override string GetDefaultOrderColumn()
        {
            return "id";
        }
    }
}
