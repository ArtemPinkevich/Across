using System.Collections.Generic;
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

public class GetDriverAssignedOrdersQueryHandler: IRequestHandler<GetDriverAssignedOrdersQuery, TransportationOrdersListDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<Transportation> _transportationRepository;
    private readonly IRepository<Entities.Truck> _truckRepository;
    private readonly IRepository<DriverRequest> _driverRequestRepository;
    private readonly UserManager<User> _userManager;
    
    public GetDriverAssignedOrdersQueryHandler(UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<Transportation> transportationRepository,
        IRepository<Entities.Truck> truckRepository,
        IRepository<DriverRequest> driverRequestRepository,
        IMapper mapper)
    {
        _userManager = userManager;
        _ordersRepository = ordersRepository;
        _transportationRepository = transportationRepository;
        _truckRepository = truckRepository;
        _driverRequestRepository = driverRequestRepository;
        _mapper = mapper;
    }
    
    public async Task<TransportationOrdersListDto> Handle(GetDriverAssignedOrdersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return CreateNoUserFoundError(request);
        
        var role = await _userManager.GetUserRole(user);
        if (role != UserRoles.Driver)
            return CreateUndefinedRoleError(request, role);
        
        List<TransportationOrderDto> transportationOrderDtos = new List<TransportationOrderDto>();
        var transportations = await _transportationRepository.GetAllAsync(x => x.DriverId == user.Id);
        foreach (var transportation in transportations)
        {
            var order = await _ordersRepository.GetAsync(x => x.Id == transportation.TransportationOrderId &&
                                                              (x.TransportationOrderStatus ==
                                                               TransportationOrderStatus.Transporting ||
                                                               x.TransportationOrderStatus ==
                                                               TransportationOrderStatus.ManagerApproving ||
                                                               x.TransportationOrderStatus ==
                                                               TransportationOrderStatus.ShipperApproving));
            transportationOrderDtos.Add(_mapper.Map<TransportationOrderDto>(order));
        }
        
        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult()
            {
                Result = ApiResult.Success,
            },
            TransportationOrderDtos = transportationOrderDtos
        };
    }
    
    private TransportationOrdersListDto CreateNoUserFoundError(GetDriverAssignedOrdersQuery request)
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
    
    private TransportationOrdersListDto CreateUndefinedRoleError(GetDriverAssignedOrdersQuery request, string role)
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