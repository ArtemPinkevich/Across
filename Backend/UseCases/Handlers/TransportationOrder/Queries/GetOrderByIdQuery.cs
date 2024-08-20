using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Queries;

public class GetOrderByIdQuery : IRequest<TransportationOrdersListDto>
{
    public int OrderId { set; get; }
}