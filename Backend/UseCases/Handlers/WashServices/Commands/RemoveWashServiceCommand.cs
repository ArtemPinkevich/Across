namespace UseCases.Handlers.WashServices.Commands
{
    using MediatR;
    using UseCases.Handlers.WashServices.Dto;

    public class RemoveWashServiceCommand : IRequest<RemoveWashServiceResultDto>
    {
        public int RemovedWashServiceId { get; set; }
        public RemoveWashServiceCommand(int removedWashServiceId)
        {
            RemovedWashServiceId = removedWashServiceId;
        }
    }
}
