namespace UseCases.Handlers.PriceGroups.Commands
{
    using MediatR;
    using UseCases.Handlers.PriceGroups.Dto;

    public class AddOrUpdatePriceGroupCommand : IRequest<AddOrUpdatePriceGroupResultDto>
    {
        public int CarWashId { get; }

        public PriceGroupDto PriceGroup { get; }

        public AddOrUpdatePriceGroupCommand(int carWashId, PriceGroupDto priceGroup)
        {
            CarWashId = carWashId;
            PriceGroup = priceGroup;
        }
    }
}
