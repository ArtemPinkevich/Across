using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Entities;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Mappings;

public class TruckAutoMapperProfile : Profile
{
    public TruckAutoMapperProfile()
    {
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
        
        CreateMap<TruckDto, Entities.Truck>()
            .ReverseMap();
    }
}