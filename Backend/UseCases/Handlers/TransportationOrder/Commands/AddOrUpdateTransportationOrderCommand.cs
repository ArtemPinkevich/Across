using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class AddOrUpdateTransportationOrderCommand:IRequest<TransportationOrderResult>
{
    public string UserId { set; get; }
    
    public TransportationOrderDto TransportationOrderDto { set; get; }
}