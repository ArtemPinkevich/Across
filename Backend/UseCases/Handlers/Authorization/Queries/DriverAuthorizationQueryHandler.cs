using System.Threading;
using System.Threading.Tasks;
using Entities;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using UseCases.Exceptions;
using UseCases.Handlers.Common;
using UseCases.Handlers.Common.Extensions;

namespace UseCases.Handlers.Authorization.Queries;

public class DriverAuthorizationQueryHandler : IRequestHandler<DriverAuthorizationQuery, AuthorizationDto>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISmsGateway _smsGateway;

    public DriverAuthorizationQueryHandler(UserManager<User> userManager,
        IOptions<JwtConfiguration> options,
        SignInManager<User> signInManager,
        IHttpContextAccessor httpContextAccessor,
        ISmsGateway smsGateway)
    {
        _userManager = userManager;
        _jwtGenerator = new JwtGenerator(options.Value);
        _httpContextAccessor = httpContextAccessor;
        _smsGateway = smsGateway;
    }

    public async Task<AuthorizationDto> Handle(DriverAuthorizationQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Phone);
        if (user == null)
            throw new NotAuthorizedException { AuthorizationMessage = $"No such user {request.Phone}" };

        var requestCookiePhoneConfirmed = _httpContextAccessor.HttpContext.Request.Cookies[Constants.PhoneConfirmed];
        if (requestCookiePhoneConfirmed == null && requestCookiePhoneConfirmed != true.ToString())
            throw new NotAuthorizedException { AuthorizationMessage = $"At cookies phone number is not confirmed {request.Phone}" };
        
        var isPhoneConfirmed = await _userManager.IsPhoneNumberConfirmedAsync(user);
        if (!isPhoneConfirmed)
            throw new NotAuthorizedException { AuthorizationMessage = $"Phone number is not confirmed {user.UserName}" };
        
        var userRole = await _userManager.GetUserRole(user);
        if (userRole == null)
            throw new NotAuthorizedException { AuthorizationMessage = $"Error user role identification {user.UserName}" };
        
        _httpContextAccessor.HttpContext.Response.Cookies.Append(Constants.RefreshTokenKey, _jwtGenerator.CreateRefreshToken(user));
        
        return new AuthorizationDto
        {
            AccessToken = _jwtGenerator.CreateAccessToken(user, userRole),
        };
    }
}