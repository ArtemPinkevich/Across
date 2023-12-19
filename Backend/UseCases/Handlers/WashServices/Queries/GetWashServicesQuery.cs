namespace UseCases.Handlers.WashServices.Queries
{
    using MediatR;
    using UseCases.Handlers.WashServices.Dto;

    public class GetWashServicesQuery : IRequest<GetWashServicesDto>
    {
        public int CarWashId { get; }
        public bool IncludeDisabled { get; }

        public GetWashServicesQuery(int carWashId, bool includeDisabled)
        {
            CarWashId = carWashId;
            IncludeDisabled = includeDisabled;
        }
    }
}
