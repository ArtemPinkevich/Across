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
    private readonly IRepository<TransferChangeStatusRecord> _transferStatusRepository;

    public StartShipperApprovingCommandHandler(IRepository<TransferChangeStatusRecord> transferStatusRepository)
    {
        _transferStatusRepository = transferStatusRepository;
    }
    
    public async Task<TransportationOrderResult> Handle(StartShipperApprovingCommand request, CancellationToken cancellationToken)
    {
        await _transferStatusRepository.AddAsync(new List<TransferChangeStatusRecord>() { new TransferChangeStatusRecord()
        {
            ChangeDatetime = DateTime.Now,
            TransportationOrderId = request.TransportationOrderId,
            TransportationStatus = TransportationStatus.ShipperApproving
        } });

        await _transferStatusRepository.SaveAsync();

        return new TransportationOrderResult() { Result = ApiResult.Success };
    }
}
