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
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Profiles.Helpers;
using UseCases.Handlers.Search.Dto;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchRecommendationsQueryHandler : IRequestHandler<SearchRecommendationsQuery, RecommendationsResultDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<Entities.Truck> _trucksRepository;
    private readonly IRepository<Transportation> _transportationRepository;

    public SearchRecommendationsQueryHandler(IMapper mapper,
        UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<Entities.Truck> trucksRepository,
        IRepository<Transportation> transportationRepository)
    {
        _mapper = mapper;
        _userManager = userManager;
        _ordersRepository = ordersRepository;
        _trucksRepository = trucksRepository;
        _transportationRepository = transportationRepository;
    }
    
    public async Task<RecommendationsResultDto> Handle(SearchRecommendationsQuery request, CancellationToken cancellationToken)
    {
        var recommendationsResultDto = new RecommendationsResultDto()
        {
            Recommendations = new List<CorrelationDto>()
        };
        
        var orders = await _ordersRepository.GetAllAsync(x => x.TransportationOrderStatus == TransportationOrderStatus.CarrierFinding);
        foreach (var order in orders)
        {
            recommendationsResultDto.Recommendations.AddRange(await FindCorrelationsForOrder(order));
        }

        recommendationsResultDto.Result = ApiResult.Success;
        return recommendationsResultDto;
    }

    private async Task<List<CorrelationDto>> FindCorrelationsForOrder(Entities.TransportationOrder order)
    {
        List<CorrelationDto> correlationDtos = new List<CorrelationDto>();
        //находим все машины подходящие по фильтру, также убираются все грузовики которые уже в заявках у заказа
        var trucks = await _trucksRepository.GetAllAsync(x => x.LoadingType == order.TruckRequirements.LoadingType
                                                              && order.DriverRequests.All(driverRequest => driverRequest.TruckId != x.Id)
                                                              && x.IsActive);
            
        var shipper = await _userManager.FindByIdAsync(order.ShipperId);
        foreach (var truck in trucks)
        {
            if(!await IsTruckValidForOrder(truck, order))
                continue;

            var driver = await _userManager.FindByIdAsync(truck.DriverId);
            correlationDtos.Add(new CorrelationDto()
            {
                Driver = await driver.ConvertToProfileDtoAsync(_userManager, _mapper),
                Shipper = await shipper.ConvertToProfileDtoAsync(_userManager, _mapper),
                TransportationOrder = _mapper.Map<TransportationOrderDto>(order),
                Truck = _mapper.Map<TruckDto>(truck)
            });
        }

        return correlationDtos;
    }

    private async Task<bool> IsTruckValidForOrder(Entities.Truck truck, Entities.TransportationOrder orderToCheck)
    {
        var transportation = await _transportationRepository.GetAsync(x => x.TruckId == truck.Id);
        //если нет transportation значит машина не назначена на заказ
        if(transportation == null)
            return true;

        var orderInProgress =  await _ordersRepository.GetAsync(x => x.Id == transportation.TransportationOrderId);
        if (DateTime.TryParse(orderInProgress.TransportationInfo.LoadDateTo, out DateTime arrivingDate))
        {
            return false;
        }
        if (DateTime.TryParse(orderToCheck.TransportationInfo.LoadDateFrom, out DateTime requiredDate))
        {
            return false;
        }

        //проверка даты прибытия заказа в работе и требуемого заказа
        //проверка локации
        return DateTime.Compare(arrivingDate, requiredDate) <= 1  && orderInProgress.TransportationInfo.UnloadingLocalityName == orderToCheck.TransportationInfo.LoadingLocalityName;
    }
}