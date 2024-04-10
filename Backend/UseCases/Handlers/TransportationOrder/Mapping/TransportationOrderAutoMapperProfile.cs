using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Entities;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Mapping;

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
            var requirement = new CarBodyRequirement
            {
                TentTruck = value.Any(x => x == CarBodyType.TentTruck),
                Container = value.Any(x => x == CarBodyType.Container),
                Van = value.Any(x => x == CarBodyType.Van),
                AllMetal = value.Any(x => x == CarBodyType.AllMetal),
                Isothermal = value.Any(x => x == CarBodyType.Isothermal),
                Refrigerator = value.Any(x => x == CarBodyType.Refrigerator),
                RefrigeratorMult = value.Any(x => x == CarBodyType.RefrigeratorMult),
                BulkheadRefr = value.Any(x => x == CarBodyType.BulkheadRefr),
                MeatRailsRef = value.Any(x => x == CarBodyType.MeatRailsRef),
                Flatbed = value.Any(x => x == CarBodyType.Flatbed),
                Opentop = value.Any(x => x == CarBodyType.Opentop),
                Opentrailer = value.Any(x => x == CarBodyType.Opentrailer),
                DumpTruck = value.Any(x => x == CarBodyType.DumpTruck),
                Barge = value.Any(x => x == CarBodyType.Barge),
                Dolly = value.Any(x => x == CarBodyType.Dolly),
                DollyPlat = value.Any(x => x == CarBodyType.DollyPlat),
                Adjustable = value.Any(x => x == CarBodyType.Adjustable),
                Tral = value.Any(x => x == CarBodyType.Tral),
                BeamTruckNgb = value.Any(x => x == CarBodyType.BeamTruckNgb),
                Bus = value.Any(x => x == CarBodyType.Bus),
                Autocart = value.Any(x => x == CarBodyType.Autocart),
                Autotower = value.Any(x => x == CarBodyType.Autotower),
                AutoCarrier = value.Any(x => x == CarBodyType.AutoCarrier),
                ConcreteTruck = value.Any(x => x == CarBodyType.ConcreteTruck),
                BitumenTruck = value.Any(x => x == CarBodyType.BitumenTruck),
                FuelTank = value.Any(x => x == CarBodyType.FuelTank),
                OffRoader = value.Any(x => x == CarBodyType.OffRoader),
                Gas = value.Any(x => x == CarBodyType.Gas),
                GrainTruck = value.Any(x => x == CarBodyType.GrainTruck),
                HorseTruck = value.Any(x => x == CarBodyType.HorseTruck),
                ContainerTrail = value.Any(x => x == CarBodyType.ContainerTrail),
                FurageTuck = value.Any(x => x == CarBodyType.FurageTuck),
                Crane = value.Any(x => x == CarBodyType.Crane),
                TimberTruck = value.Any(x => x == CarBodyType.TimberTruck),
                ScrapTruck = value.Any(x => x == CarBodyType.ScrapTruck),
                Manipulator = value.Any(x => x == CarBodyType.Manipulator),
                Microbus = value.Any(x => x == CarBodyType.Microbus),
                FlourTruck = value.Any(x => x == CarBodyType.FlourTruck),
                PanelsTruck = value.Any(x => x == CarBodyType.PanelsTruck),
                Pickup = value.Any(x => x == CarBodyType.Pickup),
                Ripetruck = value.Any(x => x == CarBodyType.Ripetruck),
                Pyramid = value.Any(x => x == CarBodyType.Pyramid),
                RollTruck = value.Any(x => x == CarBodyType.RollTruck),
                Tractor = value.Any(x => x == CarBodyType.Tractor),
                Cattle = value.Any(x => x == CarBodyType.Cattle),
                Innloader = value.Any(x => x == CarBodyType.Innloader),
                PipeTruck = value.Any(x => x == CarBodyType.PipeTruck),
                CementTruck = value.Any(x => x == CarBodyType.CementTruck),
                TankerTruck = value.Any(x => x == CarBodyType.TankerTruck),
                ChipTruck = value.Any(x => x == CarBodyType.ChipTruck),
                Wrecker = value.Any(x => x == CarBodyType.Wrecker),
                DualPurpose = value.Any(x => x == CarBodyType.DualPurpose),
                Klyushkovoz = value.Any(x => x == CarBodyType.Klyushkovoz),
                GarbageTruck = value.Any(x => x == CarBodyType.GarbageTruck),
                Jumbo = value.Any(x => x == CarBodyType.Jumbo),
                TankContainer20 = value.Any(x => x == CarBodyType.TankContainer20),
                TankContainer40 = value.Any(x => x == CarBodyType.TankContainer40),
                Mega = value.Any(x => x == CarBodyType.Mega),
                Doppelstock = value.Any(x => x == CarBodyType.Doppelstock),
                SlidingSemiTrailer2040 = value.Any(x => x == CarBodyType.SlidingSemiTrailer2040)
            };

            return requirement;
        });
        
        CreateMap<CarBodyRequirement, CarBodyType[]>().ConvertUsing((value, dest) =>
        {
            var carBodyTypes = new List<CarBodyType>();
            if(value.TentTruck)
                carBodyTypes.Add(CarBodyType.TentTruck);
            if(value.Container)
                carBodyTypes.Add(CarBodyType.Container);
            if(value.Van)
                carBodyTypes.Add(CarBodyType.Van);
            if(value.AllMetal)
                carBodyTypes.Add(CarBodyType.AllMetal);
            if(value.Isothermal)
                carBodyTypes.Add(CarBodyType.Isothermal);
            if(value.Refrigerator)
                carBodyTypes.Add(CarBodyType.Refrigerator);
            if(value.RefrigeratorMult)
                carBodyTypes.Add(CarBodyType.RefrigeratorMult);
            if(value.BulkheadRefr)
                carBodyTypes.Add(CarBodyType.BulkheadRefr);
            if(value.MeatRailsRef)
                carBodyTypes.Add(CarBodyType.MeatRailsRef);
            if(value.Flatbed)
                carBodyTypes.Add(CarBodyType.Flatbed);
            if(value.Opentop)
                carBodyTypes.Add(CarBodyType.Opentop);
            if(value.Opentrailer)
                carBodyTypes.Add(CarBodyType.Opentrailer);
            if(value.DumpTruck)
                carBodyTypes.Add(CarBodyType.DumpTruck);
            if(value.Barge)
                carBodyTypes.Add(CarBodyType.Barge);
            if(value.Dolly)
                carBodyTypes.Add(CarBodyType.Dolly);
            if(value.DollyPlat)
                carBodyTypes.Add(CarBodyType.DollyPlat);
            if (value.Adjustable)
                carBodyTypes.Add(CarBodyType.Adjustable);
            if (value.Tral)
                carBodyTypes.Add(CarBodyType.Tral);
            if (value.BeamTruckNgb)
                carBodyTypes.Add(CarBodyType.BeamTruckNgb);
            if (value.Bus)
                carBodyTypes.Add(CarBodyType.Bus);
            if(value.Autocart)
                carBodyTypes.Add(CarBodyType.Autocart);
            if(value.Autotower)
                carBodyTypes.Add(CarBodyType.Autotower);
            if(value.AutoCarrier)
                carBodyTypes.Add(CarBodyType.AutoCarrier);
            if(value.ConcreteTruck)
                carBodyTypes.Add(CarBodyType.ConcreteTruck);
            if(value.BitumenTruck)
                carBodyTypes.Add(CarBodyType.BitumenTruck);
            if(value.FuelTank)
                carBodyTypes.Add(CarBodyType.FuelTank);
            if(value.OffRoader)
                carBodyTypes.Add(CarBodyType.OffRoader);
            if(value.Gas)
                carBodyTypes.Add(CarBodyType.Gas);
            if(value.GrainTruck)
                carBodyTypes.Add(CarBodyType.GrainTruck);
            if(value.HorseTruck)
                carBodyTypes.Add(CarBodyType.HorseTruck);
            if(value.ContainerTrail)
                carBodyTypes.Add(CarBodyType.ContainerTrail);
            if(value.FurageTuck)
                carBodyTypes.Add(CarBodyType.FurageTuck);
            if(value.Crane)
                carBodyTypes.Add(CarBodyType.Crane);
            if(value.TimberTruck)
                carBodyTypes.Add(CarBodyType.TimberTruck);
            if(value.ScrapTruck)
                carBodyTypes.Add(CarBodyType.ScrapTruck);
            if(value.Manipulator)
                carBodyTypes.Add(CarBodyType.Manipulator);
            if(value.Microbus)
                carBodyTypes.Add(CarBodyType.Microbus);
            if(value.FlourTruck)
                carBodyTypes.Add(CarBodyType.FlourTruck);
            if(value.PanelsTruck)
                carBodyTypes.Add(CarBodyType.PanelsTruck);
            if(value.Pickup)
                carBodyTypes.Add(CarBodyType.Pickup);
            if(value.Ripetruck)
                carBodyTypes.Add(CarBodyType.Ripetruck);
            if(value.Pyramid)
                carBodyTypes.Add(CarBodyType.Pyramid);
            if(value.RollTruck)
                carBodyTypes.Add(CarBodyType.RollTruck);
            if(value.Tractor)
                carBodyTypes.Add(CarBodyType.Tractor);
            if(value.Cattle)
                carBodyTypes.Add(CarBodyType.Cattle);
            if(value.Innloader)
                carBodyTypes.Add(CarBodyType.Innloader);
            if(value.PipeTruck)
                carBodyTypes.Add(CarBodyType.PipeTruck);
            if(value.CementTruck)
                carBodyTypes.Add(CarBodyType.CementTruck);
            if(value.TankerTruck)
                carBodyTypes.Add(CarBodyType.TankerTruck);
            if(value.ChipTruck)
                carBodyTypes.Add(CarBodyType.ChipTruck);
            if(value.Wrecker)
                carBodyTypes.Add(CarBodyType.Wrecker);
            if(value.DualPurpose)
                carBodyTypes.Add(CarBodyType.DualPurpose);
            if(value.Klyushkovoz)
                carBodyTypes.Add(CarBodyType.Klyushkovoz);
            if(value.GarbageTruck)
                carBodyTypes.Add(CarBodyType.GarbageTruck);
            if(value.Jumbo)
                carBodyTypes.Add(CarBodyType.Jumbo);
            if(value.TankContainer20)
                carBodyTypes.Add(CarBodyType.TankContainer20);
            if(value.TankContainer40)
                carBodyTypes.Add(CarBodyType.TankContainer40);
            if(value.Mega)
                carBodyTypes.Add(CarBodyType.Mega);
            if(value.Doppelstock)
                carBodyTypes.Add(CarBodyType.Doppelstock);
            if(value.SlidingSemiTrailer2040)
                carBodyTypes.Add(CarBodyType.SlidingSemiTrailer2040);

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