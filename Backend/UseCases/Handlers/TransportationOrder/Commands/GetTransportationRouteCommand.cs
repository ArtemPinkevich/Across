using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class GetTransportationRouteCommand : IRequest<TransportationOrderRouteResultDto>
{
    public int TransportationOrderId { set; get; }
}