namespace UseCases.Handlers.Vehicles.Mappings
{
    using AutoMapper;
    using Entities;
    using UseCases.Handlers.Records.Dto;
    using UseCases.Handlers.Vehicles.Dto;

    public class VehicleAutoMapperProfile : Profile
    {
        public VehicleAutoMapperProfile()
        {
            CreateMap<Vehicle, CarInfoDto>();
            CreateMap<CarInfoDto, Vehicle>();

            CreateMap<CarBodyDto, CarBody>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(source => source.CarBodyId));

            CreateMap<CarBody, CarBodyDto>()
                .ForMember(dest => dest.CarBodyId, opt => opt.MapFrom(source => source.Id));
        }
    }
}
