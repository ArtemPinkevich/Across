namespace UseCases.Handlers.Registration.Commands
{
    using MediatR;
    using UseCases.Handlers.Registration.Dto;

    public class CarWashRegistrationCommand : IRequest<CarWashRegistrationResult>
    {
        public string SecretPassword { set; get; }

        public string CarWashName { set; get; }

        public int UtcOffset { set; get; }
    }
}
