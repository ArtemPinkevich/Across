namespace UseCases.Handlers.Profile.Commands
{
    using MediatR;
    using UseCases.Handlers.Profile.Dto;

    public class UpdateProfileCommand : IRequest<bool>
    {
        public ProfileDto ProfileDto { get; }

        public string UserId { get; }

        public UpdateProfileCommand(string id, ProfileDto profileDto)
        {
            UserId = id;
            ProfileDto = profileDto;
        }
    }
}
