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
        var user = await _userManager.FindByNameAsync(request.Phone);
        if (user == null)
            throw new NotAuthorizedException { AuthMessage = $"No such user {request.Phone}" };

        var requestCookiePhoneConfirmed =
            _httpContextAccessor.HttpContext.Request.Cookies[Constants.PhoneConfirmed];
        if (requestCookiePhoneConfirmed == null && requestCookiePhoneConfirmed != true.ToString())
            throw new NotAuthorizedException
                { AuthMessage = $"At cookies phone number is not confirmed {request.Phone}" };

        var isPhoneConfirmed = await _userManager.IsPhoneNumberConfirmedAsync(user);
        if (!isPhoneConfirmed)
            throw new NotAuthorizedException { AuthMessage = $"Phone number is not confirmed {user.UserName}" };

        var userRole = await _userManager.GetUserRole(user);
        if (userRole == null)
            throw new NotAuthorizedException { AuthMessage = $"Error user role identification {user.UserName}" };

        _httpContextAccessor.HttpContext.Response.Cookies.Append(Constants.RefreshTokenKey,
            _jwtGenerator.CreateRefreshToken(user));

        return new AuthorizationDto
        {
            AccessToken = _jwtGenerator.CreateAccessToken(user, userRole),
        };
    }
}
    
        

