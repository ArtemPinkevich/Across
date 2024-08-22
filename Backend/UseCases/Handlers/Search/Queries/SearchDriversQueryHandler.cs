using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Search.Dto;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchDriversQueryHandler : IRequestHandler<SearchDriversQuery, SearchDriversResultDto>
{
    private readonly IRepository<Entities.Truck> _truckRepository;
    private readonly IRepository<Entities.TransportationOrder> _transportationOrdersRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    
    public SearchDriversQueryHandler(IRepository<Entities.Truck> truckRepository,
        IRepository<Entities.TransportationOrder> transportationOrdersRepository,
        UserManager<User> userManager,
        IMapper mapper)
    {
        _truckRepository = truckRepository;
        _transportationOrdersRepository = transportationOrdersRepository;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<SearchDriversResultDto> Handle(SearchDriversQuery request, CancellationToken cancellationToken)
    {
        var whereExpressions = new List<Expression<Func<Entities.Truck, bool>>>();
        
        if(request.BodyVolume.HasValue)
            whereExpressions.Add(x => x.BodyVolume == request.BodyVolume);    
        if(request.CarryingCapacity.HasValue)
            whereExpressions.Add(x => x.CarryingCapacity == request.CarryingCapacity);    
        if(request.LoadingType.HasValue)
            whereExpressions.Add(x => x.LoadingType == request.LoadingType);    
        if(request.TrailerType.HasValue)
            whereExpressions.Add(x => x.TrailerType == request.TrailerType);    
        if(request.CarBodyType.HasValue)
            whereExpressions.Add(x => x.CarBodyType == request.CarBodyType);    
        if(request.InnerBodyHeight.HasValue)
            whereExpressions.Add(x => x.InnerBodyHeight == request.InnerBodyHeight);    
        if(request.InnerBodyLength.HasValue)
            whereExpressions.Add(x => x.InnerBodyLength == request.InnerBodyLength);    
        if(request.InnerBodyWidth.HasValue)
            whereExpressions.Add(x => x.InnerBodyWidth == request.InnerBodyWidth);   
        if(!string.IsNullOrEmpty(request.DriverLocation))
            whereExpressions.Add(x => x.TruckLocation == request.DriverLocation);

        var trucks = await _truckRepository.GetAllAsync(whereExpressions);
        
        trucks.AddRange(await GetTrucksFromAssignedOrders(request, cancellationToken));

        return new SearchDriversResultDto() 
            {
                Result = ApiResult.Success, 
                Drivers = trucks.Select(x => new DriverDto()
                {
                    Name = x.User.Name,
                    Surname = x.User.Surname,
                    UserId = x.UserId,
                    Truck = _mapper.Map<TruckDto>(x)
                }).ToList() 
            };
    }

    private async Task<List<Entities.Truck>> GetTrucksFromAssignedOrders(SearchDriversQuery request, CancellationToken cancellationToken)
    {
        List<Entities.Truck> trucks = new List<Entities.Truck>();
        
        var orders = await _transportationOrdersRepository.GetAllAsync(x => x.UnloadingLocalityName == request.DriverLocation);
        var transportingOrders = orders.FindAll(x =>
            x.TransferChangeHistoryRecords.Last().TransportationStatus == TransportationStatus.Transporting);

        foreach (var order in transportingOrders)
        {
            trucks.Add(order.AssignedTruckRecords.Last().Truck);
        }

        return trucks;

    }
}