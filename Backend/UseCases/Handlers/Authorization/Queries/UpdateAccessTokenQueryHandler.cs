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

public class UpdateAccessTokenQueryHandler: IRequestHandler<UpdateAccessTokenQuery, AuthorizationDto>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public UpdateAccessTokenQueryHandler(UserManager<User> userManager,
        IOptions<JwtConfiguration> options,
        IHttpContextAccessor httpContextAccessor)
    {
        _jwtGenerator = new JwtGenerator(options.Value);
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<AuthorizationDto> Handle(UpdateAccessTokenQuery request, CancellationToken cancellationToken)
    {
        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies["refresh_token"];
        
        var claims = _jwtGenerator.GetPrincipalRefreshToken(refreshToken);
        var userId = claims.FindFirst(x => x.Type == JwtClaimsTypes.Id)!.Value;

        if (DateTime.TryParse(claims.FindFirst(x => x.Type == JwtClaimsTypes.RefreshTokenExpireDateTime)!.Value,
                out DateTime expireDateTime))
        {
            if (expireDateTime < DateTime.Now.ToUniversalTime()) throw new NotAuthorizedException() { AuthMessage = "Refresh token is expired. Please, authorize again" };
        }
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new NotAuthorizedException { AuthMessage = "No such user" };

        var userRole = await _userManager.GetUserRole(user);
        
        _httpContextAccessor.HttpContext.Response.Cookies.Append("refresh_token", _jwtGenerator.CreateRefreshToken(user));

        return new AuthorizationDto()
        {
            AccessToken = _jwtGenerator.CreateAccessToken(user, userRole),
        };
    }
}