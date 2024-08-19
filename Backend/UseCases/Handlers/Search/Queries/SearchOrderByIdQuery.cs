using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchOrderByIdQuery : IRequest<TransportationOrdersListDto>
{
    public int OrderId { set; get; }
}