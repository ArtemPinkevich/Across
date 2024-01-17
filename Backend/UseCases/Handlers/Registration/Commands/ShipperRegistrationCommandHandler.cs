using Infrastructure.Interfaces;

namespace UseCases.Handlers.Registration.Commands
{
    using Entities;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.Registration.Dto;

    public class ShipperRegistrationCommandHandler : IRequestHandler<ShipperRegistrationCommand, RegistrationDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly ISmsGateway _smsGateway;

        public ShipperRegistrationCommandHandler(UserManager<User> userManager,
            ISmsGateway smsGateway)
        {
            _userManager = userManager;
            _smsGateway = smsGateway;
        }

        public async Task<RegistrationDto> Handle(ShipperRegistrationCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Name = request.Name,
                PhoneNumber = request.Phone,
                UserName = request.Phone,
            };

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Shipper);
                
                var code = await _userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                await _smsGateway.SendSms(user.PhoneNumber, $"{code}");

                return new RegistrationDto()
                {
                    Result = RegistrationResult.Success,
                };
            }

            return new RegistrationDto()
            {
                Result = RegistrationResult.Error,
                Reasons = new string[1] { "Не удалось зарегистрировать пользователя" }
            };
        }
    }
}
