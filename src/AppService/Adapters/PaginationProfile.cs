using Accolades.Maije.AppService.Dto;
using Accolades.Maije.Domain.Entities;
using AutoMapper;

namespace Accolades.Maije.AppService.Adapters
{
    internal class PaginationProfile : Profile
    {
        public PaginationProfile()
        {
            CreateMap<PaginationRequestDto, PaginationRequest>()
                .ConstructUsing(dto => new PaginationRequest(dto.Offset, dto.Limit, new Order(dto.OrderType, dto.OrderColumnName), dto.Search));

            CreateMap(typeof(PaginationResult<>), typeof(PaginationResultDto<>));

            CreateMap<PaginationLink, PaginationLinkDto>();

            CreateMap<PaginationResultInfo, PaginationResultInfoDto>();
        }
    }
}
