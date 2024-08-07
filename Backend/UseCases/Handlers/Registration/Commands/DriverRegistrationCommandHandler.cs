using System.Linq;
using Microsoft.AspNetCore.Identity;
using DataAccess.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Handlers.Registration.Dto;
using Entities;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Registration.Commands
{
    public class DriverRegistrationCommandHandler : IRequestHandler<DriverRegistrationCommand, RegistrationDto>
    {
        private readonly UserManager<User> _userManager;

        public DriverRegistrationCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegistrationDto> Handle(DriverRegistrationCommand request, CancellationToken cancellationToken)
        {
            User user = CreateUser(request);
            IdentityResult userResult =  await _userManager.CreateAsync(user, request.Password);
            if (userResult != IdentityResult.Success)
                return CreateErrorCreateUserResult(userResult);

            IdentityResult roleResult = await _userManager.AddToRoleAsync(user, UserRoles.Driver);
            if (roleResult != IdentityResult.Success)
                return CreateErrorCreateUserResult(roleResult);
            
            return new RegistrationDto()
            {
                Result = ApiResult.Success
            };
        }

        private User CreateUser(DriverRegistrationCommand request)
        {
            return new User()
            {
                UserName = request.PhoneNumber,
                Name = request.Name,
                Surname = request.Surname,
                Patronymic = request.Patronymic,
                PhoneNumber = request.PhoneNumber
            };
        }

        private RegistrationDto CreateErrorCreateUserResult(IdentityResult result)
        {
            return new RegistrationDto()
            {
                Result = ApiResult.Failed,
                Reasons = result.Errors.Select(x => x.Description).ToArray()
            };
        }
    }
}
