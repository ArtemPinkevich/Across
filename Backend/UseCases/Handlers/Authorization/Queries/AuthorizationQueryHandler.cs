namespace UseCases.Handlers.Authorization.Queries
{
    using Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Exceptions;
    using UseCases.Handlers.Helpers;

    public class AuthorizationQueryHandler : IRequestHandler<AuthorizationQuery, AuthorizationDto>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthorizationQueryHandler(IOptions<JwtConfiguration> options,
                                            UserManager<User> userManager,
                                            SignInManager<User> signInManager)
        {
            _jwtGenerator = new JwtGenerator(options.Value);
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<AuthorizationDto> Handle(AuthorizationQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(AdminLoginPrefixHelper.AddPrefix(request.Login));
            if (user == null)
            {
                throw new NotAuthorizedException() { AuthMessage = "Нет такого пользователя" };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded)
            {
                var userRole = await GetUserRole(user);
                if (userRole == null)
                    throw new NotAuthorizedException() { AuthMessage = "Ошибка в определении роли пользователя" };
                return new AuthorizationDto()
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

            if (await _userManager.IsInRoleAsync(user, UserRoles.MobileClient))
                return UserRoles.MobileClient;
            return null;
        }
    }
}
