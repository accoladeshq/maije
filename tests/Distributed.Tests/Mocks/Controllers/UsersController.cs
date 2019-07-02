using Accolades.Maije.AppService;
using Accolades.Maije.Distributed.Tests.Mocks.Dto;
using Accolades.Maije.Distributed.WebApi;
using Microsoft.Extensions.Logging;

namespace Accolades.Maije.Distributed.Tests.Mocks.Controllers
{
    internal class UsersController : MaijeController<UserDto, int>
    {
        public UsersController(IMaijeAppService<UserDto, int> appService, ILogger<UsersController> logger) : base(appService, logger)
        {
        }

        protected override string GetDefaultOrderColumn()
        {
            return "id";
        }
    }
}
