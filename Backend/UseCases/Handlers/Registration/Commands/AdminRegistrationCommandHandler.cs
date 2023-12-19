namespace UseCases.Handlers.Registration.Commands
{
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.Registration.Dto;
    using Entities;
    using DataAccess.Interfaces;
    using System.Linq;
    using UseCases.Handlers.Helpers;

    public class AdminRegistrationCommandHandler : IRequestHandler<AdminRegistrationCommand, RegistrationDto>
    {
        private readonly UserManager<User> _userManager;
        private readonly IRepository<CarWash> _carWashRepository;

        public AdminRegistrationCommandHandler(UserManager<User> userManager,
                                               IRepository<CarWash> carWashRepository)
        {
            _userManager = userManager;
            _carWashRepository = carWashRepository;
        }

        public async Task<RegistrationDto> Handle(AdminRegistrationCommand request, CancellationToken cancellationToken)
        {
            if (await _userManager.FindByNameAsync(AdminLoginPrefixHelper.AddPrefix(request.Login)) != null)
                return CreateErrorResult(new string[1] { "Пользователь с таким логином уже существует" });

            var carWash = await _carWashRepository.GetAsync(item => item.Id == request.CarWashId);
            if (carWash == null)
                return CreateErrorResult(new string[1] { "Нет автомойки для добавления администратора" });

            var user = CreateUser(request);

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return CreateErrorResult(result.Errors.Select(item => item.Description).ToArray());

            await _userManager.AddToRoleAsync(user, UserRoles.Admin);

            carWash.Users.Add(user);
            await _carWashRepository.UpdateAsync(carWash);
            await _carWashRepository.SaveAsync();

            return CreateSuccessResult(new string[1] { "Администратор зарегистрирован и добавлен к автомойке" });
        }

        private RegistrationDto CreateSuccessResult(string [] reasons)
        {
            return new RegistrationDto()
            {
                Result = RegistrationResult.Success,
                Reasons = new string[1] { "Администратор зарегистрирован и добавлен к автомойке" }
            };
        }

        private RegistrationDto CreateErrorResult(string[] reasons)
        {
            return new RegistrationDto()
            {
                Result = RegistrationResult.Error,
                Reasons = reasons
            };
        }

        private User CreateUser(AdminRegistrationCommand request)
        {
            return new User()
            {
                Name = request.Name,
                PhoneNumber = request.Phone,
                UserName = AdminLoginPrefixHelper.AddPrefix(request.Login)
            };
        }
    }
}
