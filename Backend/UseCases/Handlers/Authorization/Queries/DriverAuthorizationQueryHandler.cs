using System;
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

public class DriverAuthorizationQueryHandler : IRequestHandler<DriverAuthorizationQuery, AuthorizationDto>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public DriverAuthorizationQueryHandler(UserManager<User> userManager,
        IOptions<JwtConfiguration> options,
        SignInManager<User> signInManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _jwtGenerator = new JwtGenerator(options.Value);
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<AuthorizationDto> Handle(DriverAuthorizationQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Phone);
        if (user == null) throw new NotAuthorizedException { AuthMessage = "No such user" };

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
        {
            var userRole = await _userManager.GetUserRole(user);
            if (userRole == null)
                throw new NotAuthorizedException { AuthMessage = "Error user role identification" };
            
            _httpContextAccessor.HttpContext.Response.Cookies.Append("refresh_token", _jwtGenerator.CreateRefreshToken(user));
            
            return new AuthorizationDto
            {
                AccessToken = _jwtGenerator.CreateAccessToken(user, userRole),
            };
        }

        return null;
    }
}