namespace UseCases.Handlers.WashServices.Commands
{
    using MediatR;
    using UseCases.Handlers.WashServices.Dto;

    public class AddOrUpdateWashServiceCommand : IRequest<AddOrUpdateWashServiceResultDto>
    {
        public int CarWashId { get; }

        public WashServiceDto WashService { get; }

        public AddOrUpdateWashServiceCommand(int carWashId, WashServiceDto washService)
        {
            CarWashId = carWashId;
            WashService = washService;
        }
    }
}
