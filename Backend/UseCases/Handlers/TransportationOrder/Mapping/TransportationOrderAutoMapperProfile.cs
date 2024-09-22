using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Entities;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common;
using UseCases.Handlers.Truck.Dto;

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


public class SetOrderAddressDependingOnStatus : IMappingAction<Entities.TransportationOrder, TransportationOrderDto>
{
    public void Process(Entities.TransportationOrder source, TransportationOrderDto destination, ResolutionContext context)
    {
        destination.TransferInfo ??= new TransferInfoDto();
        destination.TransferInfo.LoadingDateFrom = source.LoadDateFrom;
        destination.TransferInfo.LoadingDateTo = source.LoadDateTo;
        
        if (destination.TransportationOrderStatus == TransportationOrderStatus.ManagerApproving
            || destination.TransportationOrderStatus == TransportationOrderStatus.ShipperApproving
            || destination.TransportationOrderStatus == TransportationOrderStatus.WaitingForLoading
            || destination.TransportationOrderStatus == TransportationOrderStatus.Loading
            || destination.TransportationOrderStatus == TransportationOrderStatus.Transporting
            || destination.TransportationOrderStatus == TransportationOrderStatus.Unloading)
        {
            destination.TransferInfo.LoadingAddress = source.LoadingAddress;
            destination.TransferInfo.UnloadingAddress = source.UnloadingAddress;
        }
        else
        {
            destination.TransferInfo.LoadingAddress = String.Empty;
            destination.TransferInfo.UnloadingAddress = String.Empty;
        }
    }
}
public class TransportationOrderLocationConverter : IValueConverter<LocationDto, string>
{
    public string Convert(LocationDto sourceMember, ResolutionContext context)
    {
        return $"{sourceMember.Country}{Constants.LocationDelimiter}{sourceMember.Region}{Constants.LocationDelimiter}{sourceMember.City}";
    }
}

public class TransportationOrderMapperProfile : Profile
{
    public TransportationOrderMapperProfile()
    {
        CreateMap<ContactInfoDto, ContactInformation>()
            .ForMember(x => x.TransportationOrder, s => s.Ignore())
            .ForMember(x => x.TransportationOrderId, s => s.Ignore())
            .ReverseMap();
        
        CreateMap<CargoDto, Entities.Cargo>()
            .ForMember(x => x.TransportationOrder, s => s.Ignore())
            .ForMember(x => x.TransportationOrderId, s => s.Ignore())
            .ReverseMap();
        
        CreateMap<TruckRequirementsDto, TruckRequirements>()
            .ForMember(d => d.CarBodyRequirement, opt => opt.MapFrom(s => s.CarBodies))
            .ForMember(d => d.LoadingType, opt => opt.MapFrom(s => s.LoadingTypeDtos))
            .ForMember(d => d.UnloadingType, opt => opt.MapFrom(s => s.UnloadingTypeDtos))
            .ForMember(d => d.TransportationOrder, opt => opt.Ignore())
            .ForMember(d => d.TransportationOrderId, opt => opt.Ignore())
            .ForMember(d => d.Id, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(d => d.CarBodies, opt => opt.MapFrom(s => s.CarBodyRequirement))
            .ForMember(d => d.LoadingTypeDtos, opt => opt.MapFrom(s => s.LoadingType))
            .ForMember(d => d.UnloadingTypeDtos, opt => opt.MapFrom(s => s.UnloadingType));
        
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
            if (value == null)
            {
                return carBodyTypes.ToArray();
            }
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

        CreateMap<LoadingTypeDto[], LoadingType>().ConvertUsing((value, dest) =>
        {
            LoadingType loadingType = LoadingType.None;
            
            if (value.Any(x => x == LoadingTypeDto.Apparels))
                loadingType |= LoadingType.Apparels;
            if (value.Any(x => x == LoadingTypeDto.Electric))
                loadingType |= LoadingType.Electric;
            if (value.Any(x => x == LoadingTypeDto.Full))
                loadingType |= LoadingType.Full;
            if (value.Any(x => x == LoadingTypeDto.Hydraulic))
                loadingType |= LoadingType.Hydraulic;
            if (value.Any(x => x == LoadingTypeDto.Hydroboard))
                loadingType |= LoadingType.Hydroboard;
            if (value.Any(x => x == LoadingTypeDto.Pneumatic))
                loadingType |= LoadingType.Pneumatic;
            if (value.Any(x => x == LoadingTypeDto.Pour))
                loadingType |= LoadingType.Pour;
            if (value.Any(x => x == LoadingTypeDto.Rear))
                loadingType |= LoadingType.Rear;
            if (value.Any(x => x == LoadingTypeDto.Side))
                loadingType |= LoadingType.Side;
            if (value.Any(x => x == LoadingTypeDto.Top))
                loadingType |= LoadingType.Top;
            if (value.Any(x => x == LoadingTypeDto.Unspecified))
                loadingType |= LoadingType.Unspecified;
            if (value.Any(x => x == LoadingTypeDto.DieselCompressor))
                loadingType |= LoadingType.DieselCompressor;
            if (value.Any(x => x == LoadingTypeDto.WithBoards))
                loadingType |= LoadingType.WithBoards;
            if (value.Any(x => x == LoadingTypeDto.WithCrate))
                loadingType |= LoadingType.WithCrate;
            if (value.Any(x => x == LoadingTypeDto.WithoutGates))
                loadingType |= LoadingType.WithoutGates;
            if (value.Any(x => x == LoadingTypeDto.SideBySide))
                loadingType |= LoadingType.SideBySide;
            if (value.Any(x => x == LoadingTypeDto.WithRemovablePillars))
                loadingType |= LoadingType.WithRemovablePillars;
            if (value.Any(x => x == LoadingTypeDto.WithSlidingRoof))
                loadingType |= LoadingType.WithSlidingRoof;
            
            return loadingType;
        });
        
        CreateMap<LoadingType, LoadingTypeDto[]>().ConvertUsing((value, dest) =>
        {
            var loadingTypeDto = new List<LoadingTypeDto>();
            
            if (value.HasFlag(LoadingType.Apparels))
                loadingTypeDto.Add(LoadingTypeDto.Apparels);
            if (value.HasFlag(LoadingType.Electric))
                loadingTypeDto.Add(LoadingTypeDto.Electric);
            if (value.HasFlag(LoadingType.Full))
                loadingTypeDto.Add(LoadingTypeDto.Full);
            if (value.HasFlag(LoadingType.Hydraulic))
                loadingTypeDto.Add(LoadingTypeDto.Hydraulic);
            if (value.HasFlag(LoadingType.Hydroboard))
                loadingTypeDto.Add(LoadingTypeDto.Hydroboard);
            if (value.HasFlag(LoadingType.Pneumatic))
                loadingTypeDto.Add(LoadingTypeDto.Pneumatic);
            if (value.HasFlag(LoadingType.Pour))
                loadingTypeDto.Add(LoadingTypeDto.Pour);
            if (value.HasFlag(LoadingType.Rear))
                loadingTypeDto.Add(LoadingTypeDto.Rear);
            if (value.HasFlag(LoadingType.Side))
                loadingTypeDto.Add(LoadingTypeDto.Side);
            if (value.HasFlag(LoadingType.Top))
                loadingTypeDto.Add(LoadingTypeDto.Top);
            if (value.HasFlag(LoadingType.Unspecified))
                loadingTypeDto.Add(LoadingTypeDto.Unspecified);
            if (value.HasFlag(LoadingType.DieselCompressor))
                loadingTypeDto.Add(LoadingTypeDto.DieselCompressor);
            if (value.HasFlag(LoadingType.WithBoards))
                loadingTypeDto.Add(LoadingTypeDto.WithBoards);
            if (value.HasFlag(LoadingType.WithCrate))
                loadingTypeDto.Add(LoadingTypeDto.WithCrate);
            if (value.HasFlag(LoadingType.WithoutGates))
                loadingTypeDto.Add(LoadingTypeDto.WithoutGates);
            if (value.HasFlag(LoadingType.SideBySide))
                loadingTypeDto.Add(LoadingTypeDto.SideBySide);
            if (value.HasFlag(LoadingType.WithRemovablePillars))
                loadingTypeDto.Add(LoadingTypeDto.WithRemovablePillars);
            if (value.HasFlag(LoadingType.WithSlidingRoof))
                loadingTypeDto.Add(LoadingTypeDto.WithSlidingRoof);

            return loadingTypeDto.ToArray();
        });

        CreateMap<TransportationOrderDto, Entities.TransportationOrder>()
            .ForMember(d => d.Id,
                opt => opt.MapFrom(s => s.TransportationOrderId))
            .ForMember(d => d.LoadingAddress,
                opt => opt.MapFrom(s => s.TransferInfo.LoadingAddress))
            .ForMember(d => d.UnloadingAddress,
                opt => opt.MapFrom(s => s.TransferInfo.UnloadingAddress))
            .ForMember(d => d.LoadingLocalityName,
                opt => opt.ConvertUsing(new TransportationOrderLocationConverter(),
                    src => src.TransferInfo.LoadingPlace))
            .ForMember(d => d.UnloadingLocalityName,
                opt => opt.ConvertUsing(new TransportationOrderLocationConverter(),
                    src => src.TransferInfo.UnloadingPlace))
            .ForMember(d => d.LoadDateFrom,
                opt => opt.MapFrom(s => s.TransferInfo.LoadingDateFrom))
            .ForMember(d => d.LoadDateTo,
                opt => opt.MapFrom(s => s.TransferInfo.LoadingDateTo))
            .ForMember(d => d.Cargo, opt => opt.MapFrom(s => s.Cargo))
            .ForMember(d => d.TruckRequirements,
                opt => opt.MapFrom(s => s.Cargo.TruckRequirements))
            .ForMember(d => d.ContactInformation,
                opt => opt.MapFrom(s => s.ContactInfoDto))
            .ForMember(d => d.Shipper, opt => opt.Ignore())
            .ForMember(d => d.ShipperId, opt => opt.Ignore())
            .ForMember(d => d.DriverRequests, opt => opt.Ignore())
            .AfterMap<CreateDefaultPropertiesAtTransportationOrder>()
            .ReverseMap()
            .ForMember(d => d.Cargo, 
                opt => opt.MapFrom(s => s.Cargo))
            .ForMember(d => d.ContactInfoDto,
                opt => opt.MapFrom(s => s.ContactInformation))
            .ForPath(d => d.Cargo.TruckRequirements,
                opt => opt.MapFrom(s => s.TruckRequirements))
            .ForMember(s => s.TransportationOrderStatus,
                opt => opt.MapFrom(d => d.TransportationOrderStatus))
            .ForMember(s => s.Price,
                opt => opt.MapFrom(d => d.Price))
            .ForPath(s => s.TransferInfo.LoadingPlace,
                opt => opt.MapFrom(d => ConvertLocationReverse(d.LoadingLocalityName)))
            .ForPath(s => s.TransferInfo.UnloadingPlace,
                opt => opt.MapFrom(d => ConvertLocationReverse(d.UnloadingLocalityName)))
            .AfterMap<SetOrderAddressDependingOnStatus>();
    }

    public static LocationDto ConvertLocationReverse(string location)
    {
        var res = location.Split(Constants.LocationDelimiter);
        if (res.Length >= 3)
            return new LocationDto() { Country = res[0], Region = res[1], City = res[2], };
        return new LocationDto()
        {
            Country = "undefined",
            Region = "undefined",
            City = "undefined",
        };
    }
}