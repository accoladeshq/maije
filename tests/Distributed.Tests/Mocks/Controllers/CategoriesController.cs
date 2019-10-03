using Accolades.Maije.AppService;
using Accolades.Maije.Distributed.Tests.Mocks.Dto;
using Accolades.Maije.Distributed.WebApi;
using Microsoft.Extensions.Logging;
using System;

namespace Accolades.Maije.Distributed.Tests.Mocks.Controllers
{
    internal class CategoriesController : MaijeController<CategoryDto, Guid>
    {
        public CategoriesController(IMaijeAppService<CategoryDto, Guid> appService, ILogger logger) : base(appService, logger)
        {
        }

        protected override string GetDefaultOrderColumn()
        {
            return "id";
        }
    }
}
