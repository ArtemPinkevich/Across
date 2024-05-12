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

namespace UseCases.Handlers.TransportationOrder.Queries;

public class GetDriverTransportationOrdersQueryHandler: IRequestHandler<GetDriverTransportationOrdersQuery, TransportationOrdersListDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<TransferChangeHistoryRecord> _orderHistoryRepository;
    private readonly UserManager<User> _userManager;

    public GetDriverTransportationOrdersQueryHandler(UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<TransferChangeHistoryRecord> orderHistoryRepository,
        IMapper mapper)
    {
        _userManager = userManager;
        _ordersRepository = ordersRepository;
        _orderHistoryRepository = orderHistoryRepository;
        _mapper = mapper;
    }
    
    public async Task<TransportationOrdersListDto> Handle(GetDriverTransportationOrdersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
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

        var historyRecords = await _orderHistoryRepository.GetAllAsync(x => x.AssignedDriverId == user.Id);
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
}