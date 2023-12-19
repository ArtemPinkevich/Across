namespace UseCases.Handlers.Registration.Commands
{
    using MediatR;
    using UseCases.Handlers.Registration.Dto;

    public class SendVerificationCodeCommand : IRequest<SendVerificationCodeResult>
    {
        public string PhoneNumber { set; get; }
    }
}
