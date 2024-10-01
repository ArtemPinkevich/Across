using System.Collections.Generic;
using System.Linq;
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
using UseCases.Handlers.Search.Dto;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Search.Queries;

public class GetOrdersInProgressQueryHandler : IRequestHandler<GetOrdersInProgressQuery, OrdersInProgressResultDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<Entities.Transportation> _transportationRepository;
    private readonly IRepository<Entities.Truck> _trucksRepository;

    public GetOrdersInProgressQueryHandler(IRepository<Entities.TransportationOrder> ordersRepository,
        UserManager<User> userManager,
        IRepository<Transportation> transportationRepository,
        IRepository<Entities.Truck> trucksRepository,
        IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _userManager = userManager;
        _transportationRepository = transportationRepository;
        _trucksRepository = trucksRepository;
        _mapper = mapper;
    }
    
    public async Task<OrdersInProgressResultDto> Handle(GetOrdersInProgressQuery request, CancellationToken cancellationToken)
    {
        var ordersInProgress = new OrdersInProgressResultDto
            {
                OrdersInProgress = new List<CorrelationDto>()
            };

        var waitingForLoadingOrders = await _ordersRepository.GetAllAsync(x =>
            x.TransportationOrderStatus == TransportationOrderStatus.ManagerApproving);
        ordersInProgress.OrdersInProgress.AddRange(await ConvertCorrelationDto(waitingForLoadingOrders).ToListAsync(cancellationToken: cancellationToken));

        var shipperApprovingOrders = await _ordersRepository.GetAllAsync(x =>
            x.TransportationOrderStatus == TransportationOrderStatus.ShipperApproving);
        ordersInProgress.OrdersInProgress.AddRange(await ConvertCorrelationDto(shipperApprovingOrders).ToListAsync(cancellationToken: cancellationToken));

        var waitingForLoading = await _ordersRepository.GetAllAsync(x =>
            x.TransportationOrderStatus == TransportationOrderStatus.WaitingForLoading);
        ordersInProgress.OrdersInProgress.AddRange(await ConvertCorrelationDto(waitingForLoading).ToListAsync(cancellationToken: cancellationToken));

        var loading = await _ordersRepository.GetAllAsync(x =>
            x.TransportationOrderStatus == TransportationOrderStatus.Loading);
        ordersInProgress.OrdersInProgress.AddRange(await ConvertCorrelationDto(loading).ToListAsync(cancellationToken: cancellationToken));

        var transportingOrders = await _ordersRepository.GetAllAsync(x =>
            x.TransportationOrderStatus == TransportationOrderStatus.Transporting);
        ordersInProgress.OrdersInProgress.AddRange(await ConvertCorrelationDto(transportingOrders).ToListAsync(cancellationToken: cancellationToken));

        var unloading = await _ordersRepository.GetAllAsync(x =>
            x.TransportationOrderStatus == TransportationOrderStatus.Unloading);
        ordersInProgress.OrdersInProgress.AddRange(await ConvertCorrelationDto(unloading).ToListAsync(cancellationToken: cancellationToken));

        var delivered = await _ordersRepository.GetAllAsync(x =>
            x.TransportationOrderStatus == TransportationOrderStatus.Delivered);
        ordersInProgress.OrdersInProgress.AddRange(await ConvertCorrelationDto(delivered).ToListAsync(cancellationToken: cancellationToken));

        ordersInProgress.Result =  ApiResult.Success;
        
        return ordersInProgress;
    }

    private async IAsyncEnumerable<CorrelationDto> ConvertCorrelationDto(List<Entities.TransportationOrder> transportationOrders)
    {
        foreach (var order in transportationOrders)
        {
            var driverRequest = order.DriverRequests.FirstOrDefault(o => o.Status == DriverRequestStatus.Approved || o.Status == DriverRequestStatus.PendingShipperApprove);
            var driver = await _userManager.FindByIdAsync(driverRequest.DriverId);
            var shipper = await _userManager.FindByIdAsync(order.ShipperId);
            var truck = await _trucksRepository.GetAsync(x => x.Id == driverRequest.TruckId);
            var dto = new CorrelationDto()
            {
                Driver = await driver.ConvertToProfileDtoAsync(_userManager, _mapper),
                Shipper = await shipper.ConvertToProfileDtoAsync(_userManager, _mapper),
                Truck = _mapper.Map<TruckDto>(truck),
                TransportationOrder = _mapper.Map<TransportationOrderDto>(order)
            };
            yield return dto;
        }
    }
}