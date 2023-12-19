namespace UseCases.Handlers.CarWash.Queries
{
    using MediatR;
    using UseCases.Handlers.CarWash.Dto;

    public class UpdateCarWashSettingsCommand : IRequest<CarWashDto>
    {
        public CarWashDto CarWashDto { get; }

        public UpdateCarWashSettingsCommand(CarWashDto carWashDto)
        {
            CarWashDto = carWashDto;
        }
    }
}
