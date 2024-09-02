using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Threading;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UseCases.Exceptions;
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Profiles.Helpers;

namespace UseCases.Handlers.Profiles.Queries;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ProfileDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public GetProfileQueryHandler(UserManager<User> userManager,
        IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<ProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        User user = await _userManager.Users
            .Include(x => x.Documents)
            .FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user == null)
        {
            return null;
        }

        var userRole = await _userManager.GetUserRole(user);
        if (userRole == null)
            throw new NotAuthorizedException { ErrorCode = NotAuthorizedErrorCode.InternalServerError, AuthorizationMessage = $"Error user role identification {user.UserName}" };

        var profileDto = _mapper.Map<ProfileDto>(user);
        profileDto.Role = userRole;
        profileDto.DocumentDtos = userRole == UserRoles.Driver
            ? UserDocumentsHelper.CreateDriverDocumentsList(user)
            : UserDocumentsHelper.CreateShipperDocumentsList(user);

        return profileDto;
    }
}
