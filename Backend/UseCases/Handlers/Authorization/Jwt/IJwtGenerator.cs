using System.Security.Claims;

namespace UseCases.Handlers.Authorization
{
    using Entities;

    public interface IJwtGenerator
    {
        string CreateAccessToken(User user, string role);

        string CreateRefreshToken(User user);

        public ClaimsPrincipal GetPrincipalRefreshToken(string token);
    }
}
