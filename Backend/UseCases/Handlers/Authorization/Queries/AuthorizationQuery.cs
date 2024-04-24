namespace UseCases.Handlers.Authorization.Queries
{
    using MediatR;

    public class AuthorizationQuery: IRequest<AuthorizationDto>
    {
        public string Phone { set; get; }
    }
}
