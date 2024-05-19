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
    private readonly IRepository<TransferChangeHistoryRecord> _orderHistoryRepository;
    private readonly UserManager<User> _userManager;

    public GetTransportationOrdersQueryHandler(UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<TransferChangeHistoryRecord> orderHistoryRepository,
        IMapper mapper)
    {
        _userManager = userManager;
        _ordersRepository = ordersRepository;
        _orderHistoryRepository = orderHistoryRepository;
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
        var historyRecords = await _orderHistoryRepository.GetAllAsync(x => x.AssignedDriverId == user.Id);
#warning эту логику нужно перенести в sql запрос, чтобы это выполнялоась в БД, но пока нет такого репозитория с такой функцией.
        var transportationOrderIds = historyRecords.Select(x => x.TransportationOrderId).Distinct();
        List<TransportationOrderDto> transportationOrderDtos = new List<TransportationOrderDto>();
        foreach (var orderId in transportationOrderIds)
        {
            var order =  await _ordersRepository.GetAsync(x => x.Id == orderId);
            if(order == null)
                continue;
            
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

    private async Task<TransportationOrdersListDto> GetShipperOrders(User user)
    {
        var transportationOrders = await _ordersRepository.GetAllAsync(x => x.UserId == user.Id);

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