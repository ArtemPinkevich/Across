namespace BackendWashMe.SignalRHubs
{
    using Entities;
    using Microsoft.AspNetCore.Authorization;
    using System.Threading.Tasks;
    using UseCases.Handlers.Registration.Commands;
    using UseCases.Handlers.Registration.Dto;
    using UseCases.Handlers.Registration.Queries;

    public partial class WashMeHub
    {
        [Authorize(Roles = UserRoles.MobileClient)]
        public async Task<bool> is_phone_acked(string phoneNumber)
        {
            bool result = await _mediator.Send(new PhoneAcknowledgmentStatusQuery(phoneNumber));
            return result;
        }

        [Authorize(Roles = UserRoles.MobileClient)]
        public async Task<SendVerificationCodeResult> verify_code_to_sms_request(string phoneNumber)
        {
            var command = new SendVerificationCodeCommand() { PhoneNumber = phoneNumber };
            SendVerificationCodeResult result = await _mediator.Send(command);
            return result;
        }

        [Authorize(Roles = UserRoles.MobileClient)]
        public async Task<PhoneVerificationDto> verify_phone(MobilePhoneVerificationCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
