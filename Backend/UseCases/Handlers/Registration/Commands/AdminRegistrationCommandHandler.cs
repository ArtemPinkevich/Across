using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.Registration.Commands
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using Dto;
    using Entities;
    using DataAccess.Interfaces;
    using System.Linq;

    public class AdminRegistrationCommandHandler : IRequestHandler<AdminRegistrationCommand, RegistrationDto>
    {
        private readonly UserManager<User> _userManager;

        public AdminRegistrationCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<RegistrationDto> Handle(AdminRegistrationCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByNameAsync(request.Login) != null)
                return CreateErrorResult(new string[1] { "User exists" });

            var user = CreateUser(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return CreateErrorResult(result.Errors.Select(item => item.Description).ToArray());

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);


            return CreateSuccessResult(new [] { "Administrator regostered" });
        }

        private static RegistrationDto CreateSuccessResult(string [] reasons)
        {
            return new RegistrationDto()
            {
                Result = ApiResult.Success,
                Reasons = reasons
            };
        }

        private static RegistrationDto CreateErrorResult(string[] reasons)
        {
            return new RegistrationDto()
            {
                Result = ApiResult.Failed,
                Reasons = reasons
            };
        }

        private static User CreateUser(AdminRegistrationCommand request)
        {
            return new User()
            {
                Name = request.Name,
                PhoneNumber = request.Phone,
                UserName = request.Login
            };
        }
    }
}
