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
    private readonly IRepository<Transportation> _transportationRepository;
    private readonly IRepository<Entities.Truck> _truckRepository;

    public AssignTruckCommandHandler(IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<DriverRequest> driverRequestRepository,
        IRepository<Transportation> transportationRepository,
        IRepository<Entities.Truck> truckRepository)
    {
        _ordersRepository = ordersRepository;
        _driverRequestRepository = driverRequestRepository;
        _transportationRepository = transportationRepository;
        _truckRepository = truckRepository;
    }
    
    public async Task<TransportationOrderResult> Handle(AssignTruckCommand request, CancellationToken cancellationToken)
    {
        await UpdateDriverRequests(request);
        await UpdateCurrentStatusAndTruck(request);
        await AddTransportationForOrder(request);
        
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
            await _driverRequestRepository.UpdateAsync(declineRequest);
        }

        var toAcceptRequest = driverRequests.FirstOrDefault(x => x.TruckId == request.TruckId);
        if (toAcceptRequest != null) 
            toAcceptRequest.Status = DriverRequestStatus.Approved;
        
        await _driverRequestRepository.UpdateAsync(toAcceptRequest);
    }

    private async Task UpdateCurrentStatusAndTruck(AssignTruckCommand request)
    {
        var order = await _ordersRepository.GetAsync(x => x.Id == request.TransportationOrderId);
        order.TransportationOrderStatus = TransportationOrderStatus.WaitingForLoading;
        await _ordersRepository.UpdateAsync(order);
    }

    private async Task AddTransportationForOrder(AssignTruckCommand request)
    {
        var transportation = await _transportationRepository.GetAsync(x => x.TransportationOrderId == request.TransportationOrderId);
        var truck = await _truckRepository.GetAsync(x => x.Id == request.TruckId);
        if (transportation != null)
        {
            transportation.DriverId = truck.DriverId;
            transportation.TransportationOrderId = request.TransportationOrderId;
            transportation.TruckId = request.TruckId;
            await _transportationRepository.UpdateAsync(transportation);
            
            return;
        }
        
        await _transportationRepository.AddAsync(new List<Transportation>(){new ()
        {
            DriverId = truck.DriverId,
            TransportationOrderId = request.TransportationOrderId,
            TruckId = request.TruckId,
        }});
    }
}
