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

public class GetShipperOrdersQueryHandler: IRequestHandler<GetShipperOrdersQuery, TransportationOrdersListDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly UserManager<User> _userManager;

    public GetShipperOrdersQueryHandler(UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IMapper mapper)
    {
        _userManager = userManager;
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }
    
    public async Task<TransportationOrdersListDto> Handle(GetShipperOrdersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return CreateNoUserFoundError(request);

        var role = await _userManager.GetUserRole(user);

        if (role != UserRoles.Shipper)
            return CreateUndefinedRoleError(request, role);
        
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
    
    private TransportationOrdersListDto CreateNoUserFoundError(GetShipperOrdersQuery request)
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
    
    private TransportationOrdersListDto CreateUndefinedRoleError(GetShipperOrdersQuery request, string role)
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