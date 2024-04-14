using MediatR;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Queries
{
    public class GetProfileQuery : IRequest<ProfileDto>
    {
        public string UserId { set; get; }
    }
}