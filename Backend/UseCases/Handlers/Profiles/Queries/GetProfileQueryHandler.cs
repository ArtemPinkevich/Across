using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Threading;
using UseCases.Exceptions;
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Queries;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ProfileDto>
{
    private readonly UserManager<User> _userManager;

    public GetProfileQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return null;
        }

        var userRole = await _userManager.GetUserRole(user);
        if (userRole == null)
            throw new NotAuthorizedException { ErrorCode = NotAuthorizedErrorCode.InternalServerError, AuthorizationMessage = $"Error user role identification {user.UserName}" };

        return new ProfileDto()
        {
            Name = user.Name,
            Surname = user.Surname,
            Patronymic = user.Patronymic,
            BirthDate = user.BirthDate,
            Role = userRole,
        };
    }
}
