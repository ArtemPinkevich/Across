using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class StartShipperApprovingCommandHandler : IRequestHandler<StartShipperApprovingCommand, TransportationOrderResult>
{
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;

    public StartShipperApprovingCommandHandler(IRepository<Entities.TransportationOrder> ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }
    
    public async Task<TransportationOrderResult> Handle(StartShipperApprovingCommand request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetAsync(x => x.Id == request.TransportationOrderId);
        if (order == null)
        { 
            return new TransportationOrderResult()
            {
                Result = ApiResult.Success,
                Reasons = new []{$"no transportation order found with id {request.TransportationOrderId}"}
            };
            
        }

        order.TransportationOrderStatus = TransportationOrderStatus.ShipperApproving;
        await _ordersRepository.SaveAsync();

        return new TransportationOrderResult() { Result = ApiResult.Success };
    }
}
