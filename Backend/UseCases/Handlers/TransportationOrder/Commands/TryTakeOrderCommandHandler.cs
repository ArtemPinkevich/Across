using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using MediatR;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class TryTakeOrderCommandHandler:IRequestHandler<TryTakeOrderCommand, TransportationOrderResult>
{
    private readonly IRepository<Entities.TransportationOrder> _transportationOrdersRepository;
    private readonly IRepository<Entities.Truck> _truckRepository;

    public TryTakeOrderCommandHandler(IRepository<Entities.TransportationOrder> transportationOrdersRepository,
        IRepository<Entities.Truck> truckRepository)
    {
        _transportationOrdersRepository = transportationOrdersRepository;
        _truckRepository = truckRepository;
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

        if (order.Trucks.Any(x => x.Id == truck.Id))
        {
            return new TransportationOrderResult()
            {
                Result = ApiResult.Failed,
                Reasons = new [] { $"Truck already added to Order"}
            };
        }
        
        order.Trucks.Add(truck);
        await _transportationOrdersRepository.SaveAsync();
        return new TransportationOrderResult() { Result = ApiResult.Success };
    }
}