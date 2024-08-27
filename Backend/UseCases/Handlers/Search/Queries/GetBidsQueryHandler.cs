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

public class GetBidsQueryHandler : IRequestHandler<GetBidsQuery, BidsResultDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Entities.TransportationOrder> _transportationOrderRepository;

    public GetBidsQueryHandler(IMapper mapper,
        UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> transportationOrderRepository)
    {
        _mapper = mapper;
        _userManager = userManager;
        _transportationOrderRepository = transportationOrderRepository;
    }
    
    public async Task<BidsResultDto> Handle(GetBidsQuery request, CancellationToken cancellationToken)
    {
        var bidsResultDto = new BidsResultDto { Correlations = new List<CorrelationDto>() };

        var orders = await _transportationOrderRepository.GetFirstAsync(x => x.Trucks.Count > 0, 50);

        foreach (var order in orders)
        {
            foreach (var truck in order.Trucks)
            {
                bidsResultDto.Correlations.Add(await CreateCorrelation(order, truck));
            }
        }

        return bidsResultDto;
        
    }

    private async Task<CorrelationDto> CreateCorrelation(Entities.TransportationOrder order, Entities.Truck truck)
    {
        var shipper = order.User;
        var shipperRole = await _userManager.GetUserRole(shipper);
        var driver = _userManager.Users.FirstOrDefault(o => o.Id == truck.UserId);
        var driverRole = await _userManager.GetUserRole(driver);
        var correlation = new CorrelationDto {
            #warning create ProfileDto mapper
            Shipper = new ProfileDto()
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
            },
            Driver = new ProfileDto()
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
            },
            Truck = _mapper.Map<TruckDto>(truck),
            TransportationOrder = _mapper.Map<TransportationOrderDto>(order)
        };

        return correlation;
    }
}