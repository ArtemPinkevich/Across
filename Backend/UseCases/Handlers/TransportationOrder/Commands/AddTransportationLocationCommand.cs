using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class AddTransportationLocationCommand : IRequest<TransportationOrderResult>
{
    public int TransportationOrderId { set; get; }
    
    public string Latitude { set; get; }
    
    public string Longitude { set; get; }
}