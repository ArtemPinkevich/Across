using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Queries;

public class GetTransportationOrdersQuery : IRequest<TransportationOrdersListDto>
{
    public string UserId { set; get; }
}