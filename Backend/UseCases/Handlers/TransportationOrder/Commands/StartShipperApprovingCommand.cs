using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class StartShipperApprovingCommand : IRequest<TransportationOrderResult>
{
    public int TruckId { set; get; }
    public int TransportationOrderId { set; get; }
}