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

namespace UseCases.Handlers.TransportationOrder.Queries;

public class GetTransportationOrdersQueryHandler: IRequestHandler<GetTransportationOrdersQuery, TransportationOrdersListDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<AssignedTruckRecord> _ordersAssignedTruckRepository;
    private readonly IRepository<Entities.Truck> _truckRepository;
    private readonly IRepository<DriverRequest> _driverRequestRepository;
    private readonly UserManager<User> _userManager;

    public GetTransportationOrdersQueryHandler(UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<AssignedTruckRecord> ordersAssignedTruckRepository,
        IRepository<Entities.Truck> truckRepository,
        IRepository<DriverRequest> driverRequestRepository,
        IMapper mapper)
    {
        _userManager = userManager;
        _ordersRepository = ordersRepository;
        _ordersAssignedTruckRepository = ordersAssignedTruckRepository;
        _truckRepository = truckRepository;
        _driverRequestRepository = driverRequestRepository;
        _mapper = mapper;
    }
    
    public async Task<TransportationOrdersListDto> Handle(GetTransportationOrdersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return CreateNoUserFoundError(request);

        var role = await _userManager.GetUserRole(user);

        if (role == UserRoles.Driver)
            return await GetDriverOrders(user);
        
        if (role == UserRoles.Shipper)
            return await GetShipperOrders(user);

        return CreateUndefinedRoleError(request, role);
    }

    private async Task<TransportationOrdersListDto> GetDriverOrders(User user)
    {
        List<TransportationOrderDto> transportationOrderDtos = new List<TransportationOrderDto>();
        
        //add all TransportationOrders where driver requested
        var driverRequests = await _driverRequestRepository.GetAllAsync(x => x.DriverId == user.Id && x.Status == DriverRequestStatus.PendingReview);
        transportationOrderDtos.AddRange(driverRequests.Select(x => _mapper.Map<TransportationOrderDto>(x)));
        
        //add all TransportationOrders where driver is assigned and order is in progress
        var currentDriverOrder = await _ordersRepository.GetAllAsync(x => x.CurrentAssignedTruck.DriverId == user.Id
                                                                          && (x.CurrentTransportationOrderStatus == TransportationOrderStatus.Transporting
                                                                           || x.CurrentTransportationOrderStatus == TransportationOrderStatus.ManagerApproving
                                                                           || x.CurrentTransportationOrderStatus == TransportationOrderStatus.ShipperApproving));
        transportationOrderDtos.AddRange(currentDriverOrder.Select(x => _mapper.Map<TransportationOrderDto>(x)));
 
        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult()
            {
                Result = ApiResult.Success,
            },
            TransportationOrderDtos = transportationOrderDtos
        };
    }

    private async Task<TransportationOrdersListDto> GetShipperOrders(User user)
    {
        var transportationOrders = await _ordersRepository.GetAllAsync(x => x.ShipperId == user.Id);

        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult()
            {
                Result = ApiResult.Success,
            },
            TransportationOrderDtos = transportationOrders.Select(x => _mapper.Map<TransportationOrderDto>(x)).ToList()
        };
    }

    private TransportationOrdersListDto CreateNoUserFoundError(GetTransportationOrdersQuery request)
    {
        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult()
            {
                Result = ApiResult.Failed,
                Reasons = new string[] { $"no user found with id {request.UserId}" }
            }
        };
    }
    
    private TransportationOrdersListDto CreateUndefinedRoleError(GetTransportationOrdersQuery request, string role)
    {
        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult()
            {
                Result = ApiResult.Failed,
                Reasons = new string[] { $"user role is not Driver or Shipper role:{role} userId:{request.UserId}" }
            }
        };
    }
}