using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Entities;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Cargo.Mapping;

public class CreateDefaultPropertiesAtTransportationOrder :IMappingAction<TransportationOrderDto, Entities.TransportationOrder>
{
    public void Process(TransportationOrderDto source, Entities.TransportationOrder destination, ResolutionContext context)
    {
        destination.Cargo ??= new Entities.Cargo();
        destination.TruckRequirements ??= new TruckRequirements();
        destination.TruckRequirements.CarBodyRequirement ??= new CarBodyRequirement();
    }
}

public class CargoAutoMapperProfile : Profile
{
    public CargoAutoMapperProfile()
    {
        CreateMap<LoadDto, Entities.Cargo>()
            .ReverseMap();
        
        CreateMap<TruckRequirementsForLoadDto, TruckRequirements>()
            .ForMember(d => d.CarBodyRequirement, opt => opt.MapFrom(s => s.CarBodies))
            .ForMember(d => d.LoadingType, opt => opt.MapFrom(s => s.LoadingTypeDtos))
            .ForMember(d => d.UnloadingType, opt => opt.MapFrom(s => s.UnloadingTypeDtos))
            .ReverseMap();
        
        CreateMap<CarBodyType[], CarBodyRequirement>().ConvertUsing((value, dest) =>
        {
#warning TODO
            var requirement = new CarBodyRequirement
            {
                TentTruck = value.Any(x => x == CarBodyType.TentTruck),
                Container = value.Any(x => x == CarBodyType.Container),
                Adjustable = value.Any(x => x == CarBodyType.Adjustable),
                AllMetal = value.Any(x => x == CarBodyType.AllMetal)
            };

            return requirement;
        });
        
        CreateMap<CarBodyRequirement, CarBodyType[]>().ConvertUsing((value, dest) =>
        {
#warning TODO
            var carBodyTypes = new List<CarBodyType>();
            if (value.Adjustable)
                carBodyTypes.Add(CarBodyType.Adjustable);
            
            if(value.Autocart)
                carBodyTypes.Add(CarBodyType.Autocart);
            

            return carBodyTypes.ToArray();
        });
        
        CreateMap<TransportationOrderDto, Entities.TransportationOrder>()
            .ForMember(d => d.LoadingAddress, 
                opt => opt.MapFrom(s => s.LoadPublishInfo.LoadingAddress))
            .ForMember(d => d.UnloadingAddress, 
                opt => opt.MapFrom(s => s.LoadPublishInfo.UnloadingAddress))
            .ForMember(d => d.LoadingLocalityName,
                opt => opt.MapFrom(s => s.LoadPublishInfo.LoadingLocalityName))
            .ForMember(d => d.UnloadingLocalityName,
                opt => opt.MapFrom(s => s.LoadPublishInfo.UnloadingLocalityName))
            .ForMember(d => d.LoadDateFrom,
                opt => opt.MapFrom(s => s.LoadPublishInfo.LoadDateFrom))
            .ForMember(d => d.LoadDateTo,
                opt => opt.MapFrom(s => s.LoadPublishInfo.LoadDateTo))
            .ForMember(d => d.Cargo, opt => opt.MapFrom(s => s.Load))
            .ForMember(d => d.TruckRequirements,
                opt => opt.MapFrom(s => s.Load.TruckRequirementsForLoadDto))
            .AfterMap<CreateDefaultPropertiesAtTransportationOrder>()
            .ReverseMap();
    }
}