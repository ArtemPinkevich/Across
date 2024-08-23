using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Search.Queries;

public class GetOrdersInProgressQueryHandler : IRequestHandler<GetOrdersInProgressQuery, TransportationOrdersListDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;

    public GetOrdersInProgressQueryHandler(IRepository<Entities.TransportationOrder> ordersRepository,
        IMapper mapper)
    {
        _ordersRepository = ordersRepository;
        _mapper = mapper;
    }
    
    public async Task<TransportationOrdersListDto> Handle(GetOrdersInProgressQuery request, CancellationToken cancellationToken)
    {
        var ordersInProgress = new TransportationOrdersListDto
            {
                TransportationOrderDtos = new List<TransportationOrderDto>()
            };

        var transportingOrders = await _ordersRepository.GetAllAsync(x =>
            x.TransferChangeHistoryRecords.OrderBy(x => x.ChangeDatetime).LastOrDefault().TransportationStatus ==
            TransportationStatus.Transporting);
        ordersInProgress.TransportationOrderDtos.AddRange(transportingOrders.Select(x => _mapper.Map<TransportationOrderDto>(x)));
        
        var courierFindingOrders = await _ordersRepository.GetAllAsync(x =>
            x.TransferChangeHistoryRecords.OrderBy(x => x.ChangeDatetime).LastOrDefault().TransportationStatus ==
            TransportationStatus.CarrierFinding);
        ordersInProgress.TransportationOrderDtos.AddRange(courierFindingOrders.Select(x => _mapper.Map<TransportationOrderDto>(x)));
        
        var readyToLoadOrders = await _ordersRepository.GetAllAsync(x =>
            x.TransferChangeHistoryRecords.OrderBy(x => x.ChangeDatetime).LastOrDefault().TransportationStatus ==
            TransportationStatus.ReadyToLoad);
        ordersInProgress.TransportationOrderDtos.AddRange(readyToLoadOrders.Select(x => _mapper.Map<TransportationOrderDto>(x)));
        
        var waitingForLoadingOrders = await _ordersRepository.GetAllAsync(x =>
            x.TransferChangeHistoryRecords.OrderBy(x => x.ChangeDatetime).LastOrDefault().TransportationStatus ==
            TransportationStatus.WaitingForLoading);
        ordersInProgress.TransportationOrderDtos.AddRange(waitingForLoadingOrders.Select(x => _mapper.Map<TransportationOrderDto>(x)));

        ordersInProgress.Result = new TransportationOrderResult() { Result = ApiResult.Success };
        
        return ordersInProgress;
    }
}