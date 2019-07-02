using Accolades.Maije.AppService.Dto;
using Accolades.Maije.Domain.Entities;
using AutoMapper;

namespace Accolades.Maije.AppService.Adapters
{
    public class ListProfile : Profile
    {
        public ListProfile()
        {
            CreateMap<ListRequestDto, ListRequest>()
                .ConstructUsing(dto => new ListRequest(new Order(dto.OrderType, dto.OrderColumnName), dto.Search));
        }
    }
}
