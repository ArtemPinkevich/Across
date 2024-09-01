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
    private readonly IRepository<Entities.Truck> _trucksRepository;

    public SearchOrdersInShipperApprovingQueryHandler(IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<Entities.Truck> trucksRepository,
        UserManager<User> userManager,
        IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _trucksRepository = trucksRepository;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<OrdersInProgressResultDto> Handle(SearchOrdersInShipperApprovingQuery request, CancellationToken cancellationToken)
    {
        var ordersInProgress = new OrdersInProgressResultDto
            {
                OrdersInProgress = new List<CorrelationDto>()
            };

        var transportingOrders = await _ordersRepository.GetAllAsync(o => o.User.Id == request.UserId && o.TransferChangeHistoryRecords.OrderBy(x => x.ChangeDatetime).LastOrDefault().TransportationStatus ==
            TransportationStatus.ShipperApproving);

        ordersInProgress.OrdersInProgress.AddRange(await ConvertCorrelationDto(transportingOrders, true).ToListAsync(cancellationToken: cancellationToken));
        
        return ordersInProgress;
    }

    private async IAsyncEnumerable<CorrelationDto> ConvertCorrelationDto(List<Entities.TransportationOrder> transportationOrders, bool hasAssignedTruck)
    {
        foreach (var order in transportationOrders)
        {
            if (hasAssignedTruck &&
                (order.AssignedTruckRecords == null || order.AssignedTruckRecords.Count == 0))
            {
                throw new Exception($"At least 1 truck must be assigned for order {order.Id}");
            }

            var dto = new CorrelationDto()
            {
                Driver = await GetDriverProfileDto(order, hasAssignedTruck),
                Shipper = await GetShipperProfileDto(order),
                Truck = await GetTruckDto(order, hasAssignedTruck),
                TransportationOrder = _mapper.Map<TransportationOrderDto>(order)
            };
            yield return dto;
        }
    }

    private async Task<TruckDto> GetTruckDto(Entities.TransportationOrder order, bool hasAssignedTruck)
    {
        if (!hasAssignedTruck)
            return null;
        
        var assignedTruckRecord = order.AssignedTruckRecords.OrderBy(x => x.ChangeDatetime).Last();
        var truck = await _trucksRepository.GetAsync(x => x.Id == assignedTruckRecord.TruckId);
        return _mapper.Map<TruckDto>(truck);
    }

    private async Task<ProfileDto> GetDriverProfileDto(Entities.TransportationOrder order, bool hasAssignedTruck)
    {
        if (!hasAssignedTruck)
            return null;
        
        var driver = await _userManager.FindByIdAsync(order.UserId);
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
        var shipper = await _userManager.FindByIdAsync(order.UserId);
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