namespace BackendWashMe.SignalRHubs
{
    using Entities;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using UseCases.Handlers.Profile.Commands;
    using UseCases.Handlers.Profile.Dto;

    public partial class WashMeHub
    {
        [Authorize(Roles = UserRoles.MobileClient)]
        public async Task set_profile_settings(ProfileDto profileDto)
        {
            var userId = Context.UserIdentifier;
            await _mediator.Send(new UpdateProfileCommand(userId, profileDto));
        }
    }
}
