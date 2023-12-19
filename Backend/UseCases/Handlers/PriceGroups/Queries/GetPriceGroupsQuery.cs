namespace UseCases.Handlers.PriceGroups.Queries
{
    using MediatR;
    using UseCases.Handlers.PriceGroups.Dto;

    public class GetPriceGroupsQuery : IRequest<GetPriceGroupsResultDto>
    {
        public int CarWashId { get; }

        public GetPriceGroupsQuery(int carWashId)
        {
            CarWashId = carWashId;
        }
    }
}
