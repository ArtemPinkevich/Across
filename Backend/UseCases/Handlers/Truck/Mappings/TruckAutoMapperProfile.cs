using AutoMapper;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Mappings;

public class TruckAutoMapperProfile : Profile
{
    public TruckAutoMapperProfile()
    {
        CreateMap<TruckDto, Entities.Truck>()
            .ReverseMap();
    }
}