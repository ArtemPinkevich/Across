namespace UseCases.Handlers.Registration.Commands
{
    using Entities;
    using Infrastructure.Interfaces;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.Registration.Dto;

    public class SendVerificationCodeCommandHandler : IRequestHandler<SendVerificationCodeCommand, SendVerificationCodeResult>
    {
        private readonly UserManager<User> _userManager;
        private readonly ISmsGateway _smsGateway;

        public SendVerificationCodeCommandHandler(UserManager<User> userManager, ISmsGateway smsGateway)
        {
            _userManager = userManager;
            _smsGateway = smsGateway;
        }


        public async Task<SendVerificationCodeResult> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByNameAsync(request.PhoneNumber);
            if (user == null)
            {
                user = new User()
                {
                    Name = request.PhoneNumber,
                    PhoneNumber = request.PhoneNumber,
                    UserName = request.PhoneNumber
                };

                IdentityResult result = await _userManager.CreateAsync(user);

                #warning TODO
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.All);
                }
            }

            var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
            await _smsGateway.SendSms(user.PhoneNumber, $"{code}");

            return new SendVerificationCodeResult()
            {
                Success = true,
            };
        }
    }
}
