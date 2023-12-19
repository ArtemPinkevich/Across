namespace UseCases.Handlers.WashServices.Commands
{
    using MediatR;
    using UseCases.Handlers.PriceGroups.Dto;

    public class RemovePriceGroupCommand : IRequest<RemovePriceGroupResultDto>
    {
        public int RemovedPriceGroupId { get; set; }

        public RemovePriceGroupCommand(int removedWashServiceId)
        {
            RemovedPriceGroupId = removedWashServiceId;
        }
    }
}
