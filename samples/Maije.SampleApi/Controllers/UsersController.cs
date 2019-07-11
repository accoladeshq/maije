using Accolades.Maije.AppService;
using Accolades.Maije.AppService.Dto;
using Accolades.Maije.Distributed.WebApi;
using Accolades.Maije.SampleApi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Accolades.Maije.SampleApi.Controllers
{
    /// <summary>
    /// Controller who manage the user
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : MaijeController<UserDto, int>
    {
        /// <summary>
        /// Initialize a new <see cref="UsersController"/>
        /// </summary>
        /// <param name="appService">The application service</param>
        /// <param name="logger"></param>
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

        /// <summary>
        /// gets the default order column
        /// </summary>
        /// <returns></returns>
        protected override string GetDefaultOrderColumn()
        {
            return "id";
        }
    }
}
