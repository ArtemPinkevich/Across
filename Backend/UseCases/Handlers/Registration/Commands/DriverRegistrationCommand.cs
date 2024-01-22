namespace UseCases.Handlers.Registration.Commands
{
    using MediatR;
    using UseCases.Handlers.Registration.Dto;

    public class DriverRegistrationCommand : IRequest<RegistrationDto>
    {
        public string Name { set; get; }
        
        public string Surname { set; get; }
        
        public string Patronymic { set; get; }
        
        public string PhoneNumber { set; get; }
        
        public string Password { set; get; }
    }
}
