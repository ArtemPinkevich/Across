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
using UseCases.Handlers.Common.Extensions;

namespace UseCases.Handlers.Authorization.Queries;

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
            throw new NotAuthorizedException { AuthMessage = $"No such user {request.Phone}" };

        if (request.WithVerification)
        {
            user.PhoneNumberConfirmed = false;
            var updateResult = await _userManager.UpdateAsync(user);
            if (updateResult == IdentityResult.Failed())
                throw new NotAuthorizedException() { AuthMessage = $"Cannot update user IsPhoneNumberConfirmed field {user.UserName}" };
            
            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
            await _smsGateway.SendSms(request.Phone, code);
        }
        
        var isPhoneConfirmed = await _userManager.IsPhoneNumberConfirmedAsync(user);
        if (!isPhoneConfirmed)
            throw new NotAuthorizedException { AuthMessage = $"Phone number is not confirmed {user.UserName}" };
        
        var userRole = await _userManager.GetUserRole(user);
        if (userRole == null)
            throw new NotAuthorizedException { AuthMessage = $"Error user role identification {user.UserName}" };
        
        _httpContextAccessor.HttpContext.Response.Cookies.Append("refresh_token", _jwtGenerator.CreateRefreshToken(user));
        
        return new AuthorizationDto
        {
            AccessToken = _jwtGenerator.CreateAccessToken(user, userRole),
        };
    }
}