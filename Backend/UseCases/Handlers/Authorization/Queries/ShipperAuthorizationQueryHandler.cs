using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Entities;
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
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly JwtConfiguration _jwtConfiguration;

    public MobileAuthorizationQueryHandler(IOptions<JwtConfiguration> options,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _jwtGenerator = new JwtGenerator(options.Value);
        _jwtConfiguration = options.Value;
        _userManager = userManager;
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthorizationDto> Handle(ShipperAuthorizationQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Phone);
        if (user == null) throw new NotAuthorizedException { AuthMessage = $"No such user {request.Phone}" };

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        
        if (!result.Succeeded) throw new NotAuthorizedException { AuthMessage = $"Password is incorrect {user.UserName}" };

        var userRole = await _userManager.GetUserRole(user);
        
        if (userRole == null) throw new NotAuthorizedException { AuthMessage = $"Error user role identification {user.UserName}" };

        var res = await _userManager.UpdateAsync(user);
        if (!res.Succeeded)
        {
            throw new NotAuthorizedException { AuthMessage = $"Error updating user {user.UserName}, {res.Errors}" };
        }
        
        _httpContextAccessor.HttpContext.Response.Cookies.Append("refresh_token", _jwtGenerator.CreateRefreshToken(user));
        
        return new AuthorizationDto
        {
            AccessToken = _jwtGenerator.CreateAccessToken(user, userRole),
        };
    }
}