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
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Search.Dto;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchOrdersInShipperApprovingQueryHandler : IRequestHandler<SearchOrdersInShipperApprovingQuery, OrdersInProgressResultDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<Entities.Truck> _trucksRepository;
    private readonly IRepository<DriverRequest> _driverRequestRepository;

    public SearchOrdersInShipperApprovingQueryHandler(IRepository<Entities.TransportationOrder> ordersRepository,
        UserManager<User> userManager,
        IRepository<Entities.Truck> trucksRepository,
        IRepository<DriverRequest> driverRequestRepository,
        IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _userManager = userManager;
        _trucksRepository = trucksRepository;
        _driverRequestRepository = driverRequestRepository;
        _mapper = mapper;
    }
    
    public async Task<OrdersInProgressResultDto> Handle(SearchOrdersInShipperApprovingQuery request, CancellationToken cancellationToken)
    {
        var ordersInProgress = new OrdersInProgressResultDto
            {
                OrdersInProgress = new List<CorrelationDto>()
            };

        var transportingOrders = await _ordersRepository.GetAllAsync(o => o.Shipper.Id == request.UserId
                                                                          && o.TransportationOrderStatus == TransportationOrderStatus.ShipperApproving);

        ordersInProgress.OrdersInProgress.AddRange(await ConvertCorrelationDto(transportingOrders).ToListAsync(cancellationToken: cancellationToken));
        
        return ordersInProgress;
    }

    private async IAsyncEnumerable<CorrelationDto> ConvertCorrelationDto(List<Entities.TransportationOrder> transportationOrders)
    {
        foreach (var order in transportationOrders)
        {
            var driverRequest = await _driverRequestRepository.GetAsync(o => o.TransportationOrderId == order.Id && o.Status == DriverRequestStatus.PendingShipperApprove);
            var truck = await _trucksRepository.GetAsync(x => x.Id == driverRequest.TruckId);
            var driver = await _userManager.FindByIdWithDocuments(truck.DriverId);
            var shipper = await _userManager.FindByIdWithDocuments(order.ShipperId);
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