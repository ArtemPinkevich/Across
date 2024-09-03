using System;
using System.Collections.Generic;
using System.Linq;
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
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<DriverRequest> _driverRequestRepository;

    public AssignTruckCommandHandler(IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<DriverRequest> driverRequestRepository)
    {
        _ordersRepository = ordersRepository;
        _driverRequestRepository = driverRequestRepository;
    }
    
    public async Task<TransportationOrderResult> Handle(AssignTruckCommand request, CancellationToken cancellationToken)
    {
        await UpdateDriverRequests(request);
        await UpdateCurrentStatusAndTruck(request);
        
        await _ordersRepository.SaveAsync();

        return new TransportationOrderResult() { Result = ApiResult.Success };
    }
    
    private async Task UpdateDriverRequests(AssignTruckCommand request)
    {
        var driverRequests = await _driverRequestRepository.GetAllAsync(x => x.TransportationOrderId == request.TransportationOrderId);
        var toDeclineRequests = driverRequests.Where(x => x.TruckId != request.TruckId);
        foreach (var declineRequest in toDeclineRequests)
        {
            declineRequest.Status = DriverRequestStatus.TakenByOtherDriver;
        }

        var toAcceptRequest = driverRequests.FirstOrDefault(x => x.TruckId == request.TruckId);
        if (toAcceptRequest != null) 
            toAcceptRequest.Status = DriverRequestStatus.Approved;
    }

    private async Task UpdateCurrentStatusAndTruck(AssignTruckCommand request)
    {
        var order = await _ordersRepository.GetAsync(x => x.Id == request.TransportationOrderId);
        order.TransportationOrderStatus = TransportationOrderStatus.Transporting;
    }
}
