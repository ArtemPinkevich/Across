using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Queries;

public class GetOrdersHistoryQuery : IRequest<TransportationOrdersListDto>
{
    public string UserId { set; get; }
}