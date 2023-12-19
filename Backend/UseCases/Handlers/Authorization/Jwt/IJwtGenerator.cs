namespace UseCases.Handlers.Authorization
{
    using Entities;

    public interface IJwtGenerator
    {
        string CreateAccessToken(User user, string role);

        string CreateRefreshToken();
    }
}
