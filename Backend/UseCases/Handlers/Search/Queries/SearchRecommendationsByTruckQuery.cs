using MediatR;
using UseCases.Handlers.Search.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchRecommendationsByTruckQuery : IRequest<SearchResultDto>
{
    public int TruckId { set; get; }
}
