using Accolades.Maije.AppService;
using Accolades.Maije.Distributed.Tests.Mocks.Dto;
using Accolades.Maije.Distributed.WebApi;
using Microsoft.Extensions.Logging;

namespace Accolades.Maije.Distributed.Tests.Mocks.Controllers
{
    internal class ValueTestController : MaijeController<ValueTestDto, int>
    {
        public ValueTestController(IMaijeAppService<ValueTestDto, int> appService, ILogger logger) : base(appService, logger)
        {
        }

        protected override string GetDefaultOrderColumn()
        {
            return "id";
        }
    }
}
