using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Search.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchRecommendationsByTruckQueryHandler : IRequestHandler<SearchRecommendationsByTruckQuery, SearchResultDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.Truck> _trucksRepository;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<Transportation> _transportationsRepository;

    public SearchRecommendationsByTruckQueryHandler(IMapper mapper,
        IRepository<Entities.Truck> trucksRepository,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<Transportation> transportationsRepository)
    {
        _mapper = mapper;
        _trucksRepository = trucksRepository;
        _ordersRepository = ordersRepository;
        _transportationsRepository = transportationsRepository;
    }
    
    public async Task<SearchResultDto> Handle(SearchRecommendationsByTruckQuery request, CancellationToken cancellationToken)
    {
        var truck = await _trucksRepository.GetAsync(x => x.Id == request.TruckId && x.IsActive);
        if (truck == null)
        {
            return new SearchResultDto()
            {
                Result = ApiResult.Failed, Reasons = new[] { $"no trucks found with id {request.TruckId}" }
            };
        }

        var orders = await _ordersRepository.GetAllAsync(x =>
            x.TransportationOrderStatus == TransportationOrderStatus.CarrierFinding &&
            x.TruckRequirements.LoadingType == truck.LoadingType &&
            x.DriverRequests.All(driverRequest => driverRequest.TruckId != truck.Id));

        var filteredOrders = orders.ToAsyncEnumerable().WhereAwait(async x => await IsTruckValidForOrder(truck, x));

        return new SearchResultDto()
        {
            Result = ApiResult.Success,
            TransportationOrders = await filteredOrders.Select(x => _mapper.Map<TransportationOrderDto>(x)).ToListAsync(cancellationToken)
        };
    }
    
    private async Task<bool> IsTruckValidForOrder(Entities.Truck truck, Entities.TransportationOrder orderToCheck)
    {
        var transportation = await _transportationsRepository.GetAsync(x => x.TruckId == truck.Id);
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