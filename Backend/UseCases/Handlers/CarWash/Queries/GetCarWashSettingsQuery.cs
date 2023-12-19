namespace UseCases.Handlers.CarWash.Queries
{
    using MediatR;
    using UseCases.Handlers.CarWash.Dto;

    public class GetCarWashSettingsQuery : IRequest<CarWashDto>
    {
        public int CarWashId { get; }

        public GetCarWashSettingsQuery(int carWashId)
        {
            CarWashId = carWashId;
        }
    }
}
