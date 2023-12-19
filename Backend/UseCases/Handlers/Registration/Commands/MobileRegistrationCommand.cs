namespace UseCases.Handlers.Registration.Commands
{
    using MediatR;
    using UseCases.Handlers.Registration.Dto;

    public class MobileRegistrationCommand : IRequest<RegistrationDto>
    {
        public string Name { set; get; }

        public string Phone { set; get; }
    }
}
