using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class TryTakeOrderCommandHandler:IRequestHandler<TryTakeOrderCommand, TransportationOrderResult>
{
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Entities.TransportationOrder> _transportationOrdersRepository;
    private readonly IRepository<Entities.Truck> _truckRepository;
    private readonly IRepository<DriverRequest> _driverRequestRepository;

    public TryTakeOrderCommandHandler(UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> transportationOrdersRepository,
        IRepository<Entities.Truck> truckRepository,
        IRepository<DriverRequest> driverRequestRepository)
    {
        _userManager = userManager;
        _transportationOrdersRepository = transportationOrdersRepository;
        _truckRepository = truckRepository;
        _driverRequestRepository = driverRequestRepository;
    }
    
    public async Task<TransportationOrderResult> Handle(TryTakeOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _transportationOrdersRepository.GetAsync(x => x.Id == request.TransportationOrderId);
        var truck = await _truckRepository.GetAsync(x => x.Id == request.TruckId);
        if (order == null || truck == null)
        {
            return new TransportationOrderResult()
            {
                Result = ApiResult.Failed,
                Reasons = new [] { $"cannot find order with OrderId:{request.TransportationOrderId} and TruckId:{request.TruckId}"}
            };
        }

        var driver = await _userManager.FindByIdAsync(truck.DriverId);
        if (driver.UserStatus != UserStatus.Confirmed)
        {
            return new TransportationOrderResult()
            {
                Result = ApiResult.Failed,
                Reasons = new [] { $"driver is not confirmed"}
            };
        }
        
        DriverRequest driverRequest = new DriverRequest()
        {
            TruckId = truck.Id,
            TransportationOrderId = order.Id,
            DriverId = truck.DriverId,
            Status = DriverRequestStatus.PendingLogistReview,
            CreatedDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture)
        };

        await _driverRequestRepository.AddAsync(new List<DriverRequest>(){driverRequest});

        await _driverRequestRepository.SaveAsync();
        
        return new TransportationOrderResult() { Result = ApiResult.Success };
    }
}