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

    public SearchRecommendationsQueryHandler(IMapper mapper,
        UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<Entities.Truck> trucksRepository)
    {
        _mapper = mapper;
        _userManager = userManager;
        _ordersRepository = ordersRepository;
        _trucksRepository = trucksRepository;
    }
    
    public async Task<RecommendationsResultDto> Handle(SearchRecommendationsQuery request, CancellationToken cancellationToken)
    {
        var recommendationsResultDto = new RecommendationsResultDto()
        {
            Recommendations = new List<CorrelationDto>()
        };
        Expression <Func<Entities.TransportationOrder, bool>> searchByStatusExpression = x => x.TransportationOrderStatusRecords.OrderBy(record => record.ChangeDatetime).Last().TransportationOrderStatus == TransportationOrderStatus.CarrierFinding;
        var orders = await _ordersRepository.GetAllAsync(searchByStatusExpression);
        foreach (var order in orders)
        {
            var trucks = await _trucksRepository.GetAllAsync(x => x.TruckLocation == order.LoadingLocalityName
                                                                  && x.LoadingType == order.TruckRequirements.LoadingType
                                                                  && x.InnerBodyHeight >= order.TruckRequirements.InnerBodyHeight
                                                                  && x.CarryingCapacity >= order.TruckRequirements.CarryingCapacity
                                                                  && x.InnerBodyLength >= order.TruckRequirements.InnerBodyLength
                                                                  && x.InnerBodyWidth >= order.TruckRequirements.InnerBodyWidth);
            
            var shipper = await _userManager.FindByIdAsync(order.ShipperId);
            var shipperRole = await _userManager.GetUserRole(shipper);
            foreach (var truck in trucks)
            {
                var driver = await _userManager.FindByIdAsync(truck.DriverId);
                var driverRole = await _userManager.GetUserRole(driver);
                recommendationsResultDto.Recommendations.Add(new CorrelationDto()
                {
                    #warning Create mapper for ProfileDto
                    Driver = new ProfileDto()
                    {
                        Name = driver.Name,
                        Surname = driver.Surname,
                        Patronymic = driver.Patronymic,
                        BirthDate = driver.BirthDate,
                        PhoneNumber = driver.PhoneNumber,
                        Role = shipperRole,
                        Status = driver.UserStatus,
                        DocumentDtos = driverRole == UserRoles.Driver
                            ? UserDocumentsHelper.CreateDriverDocumentsList(driver)
                            : UserDocumentsHelper.CreateShipperDocumentsList(driver)
                    },
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
                    TransportationOrder = _mapper.Map<TransportationOrderDto>(order),
                    Truck = _mapper.Map<TruckDto>(truck)
                });
            }

        }

        return null;
    }
}