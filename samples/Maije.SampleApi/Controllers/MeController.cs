using Accolades.Maije.Crosscutting.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accolades.Maije.SampleApi.Controllers
{
    /// <summary>
    /// The me controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize]
    public class MeController : Controller
    {
        /// <summary>
        /// The user context
        /// </summary>
        private readonly IUserContext _userContext;

        /// <summary>
        /// Initialize a new <see cref="MeController"/>
        /// </summary>
        /// <param name="userContext">The user context</param>
        public MeController(IUserContext userContext)
        {
            _userContext = userContext;
        }

        /// <summary>
        /// Gets the user info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetInfo()
        {
            return Ok(new
            {
                id = _userContext.UserId
            });
        }
    }
}
