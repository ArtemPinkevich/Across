using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Entities;
using MediatR;
using UseCases.Handlers.Common;
using UseCases.Handlers.Common.Extensions;
using UseCases.Exceptions;

namespace UseCases.Handlers.Authorization.Queries;

[Obsolete]
public class AuthorizationQueryHandler : IRequestHandler<AuthorizationQuery, AuthorizationDto>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationQueryHandler(IOptions<JwtConfiguration> options,
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _jwtGenerator = new JwtGenerator(options.Value);
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthorizationDto> Handle(AuthorizationQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Login);
        if (user == null)
            throw new NotAuthorizedException { ErrorCode = NotAuthorizedErrorCode.NoUserFound, AuthorizationMessage = $"No such user {request.Login}" };

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new NotAuthorizedException { ErrorCode = NotAuthorizedErrorCode.NoUserFound, AuthorizationMessage = $"Password is not valid for login {request.Login}" };

        var userRole = await _userManager.GetUserRole(user);
        if (userRole == null)
            throw new NotAuthorizedException { ErrorCode = NotAuthorizedErrorCode.InternalServerError, AuthorizationMessage = $"Error user role identification {user.UserName}" };

        return new AuthorizationDto
        {
            AccessToken = _jwtGenerator.CreateAccessToken(user, userRole)
        };
    }
}
    
        

