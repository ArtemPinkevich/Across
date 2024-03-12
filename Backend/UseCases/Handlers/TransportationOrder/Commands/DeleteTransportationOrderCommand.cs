using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class DeleteTransportationOrderCommand : IRequest<TransportationOrderResult>
{
    public int CargoId { set; get; }
}