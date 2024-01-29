using System;
using System.Threading;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using UseCases.Exceptions;

namespace UseCases.Handlers.Authorization.Queries;

public class DriverAuthorizationQueryHandler : IRequestHandler<DriverAuthorizationQuery, AuthorizationDto>
{
    private readonly IJwtGenerator _jwtGenerator;
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public DriverAuthorizationQueryHandler(UserManager<User> userManager,
        IOptions<JwtConfiguration> options,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _jwtGenerator = new JwtGenerator(options.Value);
        _signInManager = signInManager;
    }

    public async Task<AuthorizationDto> Handle(DriverAuthorizationQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.Phone);
        if (user == null) throw new NotAuthorizedException { AuthMessage = "No such user" };

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        if (result.Succeeded)
        {
            var userRole = await GetUserRole(user);
            if (userRole == null)
                throw new NotAuthorizedException { AuthMessage = "Error user role identification" };
            return new AuthorizationDto
            {
                AccessToken = _jwtGenerator.CreateAccessToken(user, userRole),
                RefreshToken = _jwtGenerator.CreateRefreshToken(),
                ExpireDateTime = DateTime.Now.AddDays(7).ToString()
            };
        }

        return null;
    }

    private async Task<string> GetUserRole(User user)
    {
        if (await _userManager.IsInRoleAsync(user, UserRoles.Admin))
            return UserRoles.Admin;

        if (await _userManager.IsInRoleAsync(user, UserRoles.Owner))
            return UserRoles.Owner;

        if (await _userManager.IsInRoleAsync(user, UserRoles.Driver))
            return UserRoles.Driver;

        if (await _userManager.IsInRoleAsync(user, UserRoles.Shipper))
            return UserRoles.Shipper;

        if (await _userManager.IsInRoleAsync(user, UserRoles.Lawyer))
            return UserRoles.Lawyer;
        return null;
    }
}