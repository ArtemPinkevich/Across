using Entities;
using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class ChangeTransportationStatusCommand : IRequest<TransportationOrderResult>
{
    public int TransportationOrderId { set; get; }
    public TransportationOrderStatus TransportationStatus { set; get; }
}
