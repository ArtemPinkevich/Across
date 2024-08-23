using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class AssignTruckCommand : IRequest<TransportationOrderResult>
{
    public int TruckId { set; get; }
    public int TransportationOrderId { set; get; }
}