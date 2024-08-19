using MediatR;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchDriverByTruckIdQuery:IRequest<ProfileDto>
{
    public int TruckId { set; get; }
}