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

public class AssignTruckCommandHandler : IRequestHandler<AssignTruckCommand, TransportationOrderResult>
{
    private readonly IRepository<TransferAssignedTruckRecord> _ordersAssignedTruckRepository;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<TransferChangeStatusRecord> _transferStatusRepository;

    public AssignTruckCommandHandler(IRepository<TransferAssignedTruckRecord> ordersAssignedTruckRepository, IRepository<TransferChangeStatusRecord> transferStatusRepository, IRepository<Entities.TransportationOrder> ordersRepository)
    {
        _ordersAssignedTruckRepository = ordersAssignedTruckRepository;
        _transferStatusRepository = transferStatusRepository;
        _ordersRepository = ordersRepository;
    }
    
    public async Task<TransportationOrderResult> Handle(AssignTruckCommand request, CancellationToken cancellationToken)
    {
        TransferAssignedTruckRecord record = new TransferAssignedTruckRecord()
        {
            ChangeDatetime = DateTime.Now,
            TransportationOrderId = request.TransportationOrderId,
            TruckId = request.TruckId,
        };

        await _ordersAssignedTruckRepository.AddAsync(new List<TransferAssignedTruckRecord>() { record });

        await _transferStatusRepository.AddAsync(new List<TransferChangeStatusRecord>() { new TransferChangeStatusRecord()
        {
            ChangeDatetime = DateTime.Now,
            TransportationOrderId = request.TransportationOrderId,
            TransportationStatus = TransportationStatus.WaitingForLoading
        } });

        var order = await _ordersRepository.GetAsync(o => o.Id == request.TransportationOrderId);
        order.Trucks.Clear();

        await _ordersAssignedTruckRepository.SaveAsync();
        await _transferStatusRepository.SaveAsync();
        await _ordersRepository.SaveAsync();

        return new TransportationOrderResult() { Result = ApiResult.Success };
    }
}
