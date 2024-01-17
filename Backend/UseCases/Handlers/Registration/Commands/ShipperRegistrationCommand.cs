namespace UseCases.Handlers.Registration.Commands
{
    using MediatR;
    using UseCases.Handlers.Registration.Dto;

    public class ShipperRegistrationCommand : IRequest<RegistrationDto>
    {
        public string Name { set; get; }
        
        public string Surname { set; get; }
        
        public string Patronymic { set; get; }
        
        public string BirthDate { set; get; }
        
        public string Phone { set; get; }
        
    }
}
