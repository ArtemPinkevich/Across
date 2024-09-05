using System;
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
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Profiles.Helpers;
using UseCases.Handlers.Search.Dto;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchOrdersInShipperApprovingQueryHandler : IRequestHandler<SearchOrdersInShipperApprovingQuery, OrdersInProgressResultDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<Transportation> _transportationRepository;
    
    public SearchOrdersInShipperApprovingQueryHandler(IRepository<Entities.TransportationOrder> ordersRepository,
        UserManager<User> userManager,
        IRepository<Transportation> transportationRepository,
        IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _userManager = userManager;
        _transportationRepository = transportationRepository;
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
            var transportation = await _transportationRepository.GetAsync(x => x.TransportationOrderId == order.Id);
            var driver = await _userManager.FindByIdWithDocuments(transportation.DriverId);
            var shipper = await _userManager.FindByIdWithDocuments(order.ShipperId);
            var dto = new CorrelationDto()
            {
                Driver = await driver.ConvertToProfileDto(_userManager, _mapper),
                Shipper = await shipper.ConvertToProfileDto(_userManager, _mapper),
                Truck = _mapper.Map<TruckDto>(transportation.Truck),
                TransportationOrder = _mapper.Map<TransportationOrderDto>(order)
            };
            yield return dto;
        }
    }
}