using System.Threading;
using System.Threading.Tasks;
using Entities;
using Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Commands;

public class ChangePersonStatusCommandHandler : IRequestHandler<ChangePersonStatusCommand, ProfileResultDto>
{
    private readonly UserManager<User> _userManager;

    public ChangePersonStatusCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public async Task<ProfileResultDto> Handle(ChangePersonStatusCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
            return CreateErrorResult($"user not found with id {request.UserId}");

        user.UserStatus = (UserStatus)request.PersonStatus;

        await _userManager.UpdateAsync(user);

        return new ProfileResultDto() { Result = ApiResult.Success };
    }
    
    private ProfileResultDto CreateErrorResult(string errorMessage)
    {
        return new ProfileResultDto()
        {
            Result = ApiResult.Failed,
            Reasons = new[] { errorMessage }
        };
    }
}