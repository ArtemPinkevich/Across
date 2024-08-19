using MediatR;
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Search.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchShipperByOrderIdQuery : IRequest<ProfileDto>
{
    public int OrderId { set; get; }
}