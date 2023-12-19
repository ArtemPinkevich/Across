namespace UseCases.Handlers.Authorization.Queries
{
    using MediatR;

    public class MobileAuthorizationQuery: IRequest<AuthorizationDto>
    {
        public string Phone { set; get; }
    }
}
