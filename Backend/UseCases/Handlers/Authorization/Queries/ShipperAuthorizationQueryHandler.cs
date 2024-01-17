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

    public class MobileAuthorizationQueryHandler : IRequestHandler<ShipperAuthorizationQuery, AuthorizationDto>
    {
        private readonly IJwtGenerator _jwtGenerator;
        private readonly UserManager<User> _userManager;

        public MobileAuthorizationQueryHandler(IOptions<JwtConfiguration> options,
                                               UserManager<User> userManager)
        {
            _jwtGenerator = new JwtGenerator(options.Value);
            _userManager = userManager;
        }

        public async Task<AuthorizationDto> Handle(ShipperAuthorizationQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Phone);
            if (user == null)
            {
                throw new NotAuthorizedException() { AuthMessage = "Нет такого пользователя" };
            }

            AuthorizationDto auth = new AuthorizationDto();
            auth.AccessToken = _jwtGenerator.CreateAccessToken(user, UserRoles.Shipper);
            auth.RefreshToken = _jwtGenerator.CreateRefreshToken();
            auth.ExpireDateTime = DateTime.Now.AddDays(3650).ToString();
            return auth;
        }
    }
}
