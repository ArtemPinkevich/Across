using System.Threading;
using System.Threading.Tasks;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Registration.Dto;

namespace UseCases.Handlers.Registration.Commands
{
    public class MobilePhoneVerificationCommandHandler : IRequestHandler<MobilePhoneVerificationCommand, PhoneVerificationDto>
    {
        private const string NO_SUCH_USER = "No such user";

        private readonly UserManager<User> _userManager;

        public MobilePhoneVerificationCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<PhoneVerificationDto> Handle(MobilePhoneVerificationCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Phone);
            if (user == null)
                return new PhoneVerificationDto() { IsVerificated = false, Message = NO_SUCH_USER };

            var result = await _userManager.ChangePhoneNumberAsync(user, request.Phone, request.Code);
            if (result.Succeeded)
                return new PhoneVerificationDto() { IsVerificated = true };

            return new PhoneVerificationDto() { IsVerificated = false };
        }
    }
}
