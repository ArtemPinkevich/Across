using AutoMapper;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Cargo.Mapping;

public class CargoAutoMapperProfile : Profile
{
    public CargoAutoMapperProfile()
    {
        CreateMap<TruckDto, Entities.Truck>()
            .ReverseMap();
    }
}