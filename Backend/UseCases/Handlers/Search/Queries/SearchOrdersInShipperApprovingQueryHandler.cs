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
            var dto = new CorrelationDto()
            {
                Driver = await GetDriverProfileDto(order),
                Shipper = await GetShipperProfileDto(order),
                Truck = await GetTruckDto(order),
                TransportationOrder = _mapper.Map<TransportationOrderDto>(order)
            };
            yield return dto;
        }
    }

    private async Task<TruckDto> GetTruckDto(Entities.TransportationOrder order)
    {
        var transportation = await _transportationRepository.GetAsync(x => x.TransportationOrderId == order.Id);
        return _mapper.Map<TruckDto>(transportation.Truck);
    }

    private async Task<ProfileDto> GetDriverProfileDto(Entities.TransportationOrder order)
    {
        var driver = await _userManager.FindByIdAsync(order.ShipperId);
        var driverRole = await _userManager.GetUserRole(driver);

        return new ProfileDto()
        {
            Name = driver.Name,
            Surname = driver.Surname,
            Patronymic = driver.Patronymic,
            BirthDate = driver.BirthDate,
            PhoneNumber = driver.PhoneNumber,
            Role = driverRole,
            Status = driver.UserStatus,
            DocumentDtos = driverRole == UserRoles.Driver
                ? UserDocumentsHelper.CreateDriverDocumentsList(driver)
                : UserDocumentsHelper.CreateShipperDocumentsList(driver)
        };
    }
    
    private async Task<ProfileDto> GetShipperProfileDto(Entities.TransportationOrder order)
    {
        var shipper = await _userManager.FindByIdAsync(order.ShipperId);
        var shipperRole = await _userManager.GetUserRole(shipper);

        return new ProfileDto()
        {
            Name = shipper.Name,
            Surname = shipper.Surname,
            Patronymic = shipper.Patronymic,
            BirthDate = shipper.BirthDate,
            PhoneNumber = shipper.PhoneNumber,
            Role = shipperRole,
            Status = shipper.UserStatus,
            DocumentDtos = shipperRole == UserRoles.Driver
                ? UserDocumentsHelper.CreateDriverDocumentsList(shipper)
                : UserDocumentsHelper.CreateShipperDocumentsList(shipper)
        };
    }
}