namespace UseCases.Handlers.Registration.Commands
{
    using MediatR;
    using UseCases.Handlers.Registration.Dto;

    public class AdminRegistrationCommand : IRequest<RegistrationDto>
    {
        public string SecretPassword { set; get; }

        public int CarWashId { set; get; }

        public string Name { set; get; }

        public string Phone { set; get; }

        public string Login { set; get; }

        public string Password { set; get; }
    }
}
