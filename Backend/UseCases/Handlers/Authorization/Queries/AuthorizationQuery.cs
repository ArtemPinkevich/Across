namespace UseCases.Handlers.Authorization.Queries
{
    using MediatR;

    public class AuthorizationQuery: IRequest<AuthorizationDto>
    {
        public string Login { set; get; }
        
        public string Password { set; get; }
    }
}
