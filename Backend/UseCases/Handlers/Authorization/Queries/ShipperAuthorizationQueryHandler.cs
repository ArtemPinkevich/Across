using System;
using System.Globalization;
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


[Obsolete]
public class MobileAuthorizationQueryHandler : IRequestHandler<ShipperAuthorizationQuery, AuthorizationDto>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ISmsGateway _smsGateway;

    public MobileAuthorizationQueryHandler(IOptions<JwtConfiguration> options,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IHttpContextAccessor httpContextAccessor,
        ISmsGateway smsGateway)
    {
        _jwtGenerator = new JwtGenerator(options.Value);
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _smsGateway = smsGateway;
    }

    public async Task<AuthorizationDto> Handle(ShipperAuthorizationQuery request, CancellationToken cancellationToken)
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