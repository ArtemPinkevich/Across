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
using UseCases.Handlers.Common;
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
        var refreshToken = _httpContextAccessor.HttpContext.Request.Cookies[Constants.RefreshTokenKey];
        if (refreshToken == null)
            throw new NotAuthorizedException() { ErrorCode = NotAuthorizedErrorCode.InternalServerError, AuthorizationMessage = "No refresh token found at cookies" };
        
        var claims = _jwtGenerator.GetPrincipalRefreshToken(refreshToken);
        var userId = claims.FindFirst(x => x.Type == JwtClaimsTypes.Id)!.Value;
        if(userId == null)
            throw new NotAuthorizedException() { ErrorCode = NotAuthorizedErrorCode.InternalServerError, AuthorizationMessage = "No userId found at refresh token" };

        if (DateTime.TryParse(claims.FindFirst(x => x.Type == JwtClaimsTypes.RefreshTokenExpireDateTime)!.Value, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out DateTime expireDateTime))
        {
            if (expireDateTime < DateTime.Now.ToUniversalTime())
                throw new NotAuthorizedException() { ErrorCode = NotAuthorizedErrorCode.RefreshTokenExpired, AuthorizationMessage = "Refresh token is expired. Please, verify phone again" };
        }
        else
        {
            throw new NotAuthorizedException() { ErrorCode = NotAuthorizedErrorCode.InternalServerError, AuthorizationMessage = "Unable to parse refresh token expire date" };
        }
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) throw new NotAuthorizedException { AuthorizationMessage = "No such user" };

        var userRole = await _userManager.GetUserRole(user);
        
        _httpContextAccessor.HttpContext.Response.Cookies.Append(Constants.RefreshTokenKey, _jwtGenerator.CreateRefreshToken(user));

        return new AuthorizationDto()
        {
            AccessToken = _jwtGenerator.CreateAccessToken(user, userRole),
        };
    }
}