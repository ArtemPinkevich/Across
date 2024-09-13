namespace UseCases.Handlers.Registration.Commands
{
    using MediatR;
    using UseCases.Handlers.Registration.Dto;

    public class AdminRegistrationCommand : IRequest<RegistrationDto>
    {
        public string Name { set; get; }

        public string Phone { set; get; }

        public string Login { set; get; }

        public string Password { set; get; }

        public string Role { private set; get; }

        public void SetRole(string role)
        {
            Role = role;
        }
    }
}
