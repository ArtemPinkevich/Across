using MediatR;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Commands;

public class UpdateProfileCommand : IRequest<ProfileResultDto>
{
    public string UserId { set; get; }

    public ProfileDto ProfileDto { set; get; }
}
