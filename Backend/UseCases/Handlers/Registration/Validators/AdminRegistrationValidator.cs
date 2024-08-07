using FluentValidation;
using UseCases.Handlers.Registration.Commands;

namespace UseCases.Handlers.Registration.Validators
{
    public class AdminRegistrationValidator : AbstractValidator<AdminRegistrationCommand>
    {
        public AdminRegistrationValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Name is null or empty error");
            RuleFor(x => x.Login).NotEmpty().NotNull().WithMessage("Name is null or empty error");
            RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Name is null or empty error");
            RuleFor(x => x.Phone).NotEmpty().NotNull().WithMessage("Name is null or empty error");
        }
    }
}
