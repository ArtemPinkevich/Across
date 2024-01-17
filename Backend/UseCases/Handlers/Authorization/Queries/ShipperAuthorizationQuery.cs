namespace UseCases.Handlers.Authorization.Queries
{
    using MediatR;

    public class ShipperAuthorizationQuery: IRequest<AuthorizationDto>
    {
        public string Phone { set; get; }
    }
}
