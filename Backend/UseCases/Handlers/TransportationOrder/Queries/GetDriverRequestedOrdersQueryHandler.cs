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

public class GetDriverRequestedOrdersQueryHandler: IRequestHandler<GetDriverRequestedOrdersQuery, TransportationOrdersListDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<DriverRequest> _driverRequestRepository;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly UserManager<User> _userManager;

    public GetDriverRequestedOrdersQueryHandler(UserManager<User> userManager,
        IRepository<DriverRequest> driverRequestRepository,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IMapper mapper)
    {
        _userManager = userManager;
        _driverRequestRepository = driverRequestRepository;
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }
    
    public async Task<TransportationOrdersListDto> Handle(GetDriverRequestedOrdersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return CreateNoUserFoundError(request);
        
        var role = await _userManager.GetUserRole(user);
        if (role != UserRoles.Driver)
            return CreateUndefinedRoleError(request, role);
        
        List<TransportationOrderDto> transportationOrderDtos = new List<TransportationOrderDto>();

        var driverRequests = await _driverRequestRepository.GetAllAsync(x => x.DriverId == user.Id && x.Status == DriverRequestStatus.PendingLogistReview);

        foreach (var driverRequest in driverRequests)
        {
            var transportationOrder = await _ordersRepository.GetAsync(order => order.Id == driverRequest.TransportationOrderId);
            transportationOrderDtos.Add(_mapper.Map<TransportationOrderDto>(transportationOrder));
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
    
    private TransportationOrdersListDto CreateUndefinedRoleError(GetDriverRequestedOrdersQuery request, string role)
    {
        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult()
            {
                Result = ApiResult.Failed,
                Reasons = new string[] { $"user role is not Driver, role:{role} userId:{request.UserId}" }
            }
        };
    }
    
    private TransportationOrdersListDto CreateNoUserFoundError(GetDriverRequestedOrdersQuery request)
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
}