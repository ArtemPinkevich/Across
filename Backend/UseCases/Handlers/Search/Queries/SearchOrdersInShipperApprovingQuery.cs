using MediatR;
using UseCases.Handlers.Search.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchOrdersInShipperApprovingQuery : IRequest<OrdersInProgressResultDto>
{
    public string UserId { set; get; }
}