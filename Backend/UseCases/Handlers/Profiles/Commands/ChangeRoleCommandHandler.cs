using System;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using UseCases.Handlers.Authorization;
using UseCases.Handlers.Common;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Commands;

public class ChangeRoleCommandHandler : IRequestHandler<ChangeRoleCommand, ChangeRoleResultDto>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChangeRoleCommandHandler(IOptions<JwtConfiguration> options,
        UserManager<User> userManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _jwtGenerator = new JwtGenerator(options.Value);
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<ChangeRoleResultDto> Handle(ChangeRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return CreateErrorResult($"user not found with id {request.UserId}");

        var role = await _userManager.GetUserRole(user);
        var result = await _userManager.RemoveFromRoleAsync(user, role);
        if (!result.Succeeded)
            return CreateErrorResult($"cannot remove from role current role: {role} user:{request.UserId}");

        var res = await _userManager.AddToRoleAsync(user, request.Role);
        if (!res.Succeeded)
            return CreateErrorResult($"cannot add to role:{request.Role} user:{request.UserId}");

        _httpContextAccessor.HttpContext.Response.Cookies.Append(Constants.RefreshTokenKey,
            _jwtGenerator.CreateRefreshToken(user));
        
        return new ChangeRoleResultDto()
        {
            AccessToken = _jwtGenerator.CreateAccessToken(user, request.Role),
            Result = ApiResult.Success,
            Errors = null,
        };
    }

    private ChangeRoleResultDto CreateErrorResult(string errorMessage)
    {
        return new ChangeRoleResultDto()
        {
            Result = ApiResult.Failed,
            Errors = new[] { errorMessage }
        };
    }
}