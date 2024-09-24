using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class GetTransportationLocationCommand : IRequest<TransportationOrderLocationResultDto>
{
    public int TransportationOrderId { set; get; }
}