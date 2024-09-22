using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class ClarifyCommand : IRequest<TransportationOrderResult>
{
    public int TransportationOrderId { set; get; }
    public string LoadingTime { set; get; }
    public string LoadingContactPerson { set; get; }
    public string LoadingContactPhone { set; get; }
    public string UnloadingContactPerson { set; get; }
    public string UnloadingContactPhone { set; get; }
}
