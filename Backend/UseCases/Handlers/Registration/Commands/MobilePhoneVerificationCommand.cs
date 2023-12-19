using MediatR;
using UseCases.Handlers.Registration.Dto;

namespace UseCases.Handlers.Registration.Commands
{
    public class MobilePhoneVerificationCommand : IRequest<PhoneVerificationDto>
    {
        public string UserId { set; get; }
        public string Phone { set; get; }
        public string Code { set; get; }
    }
}
