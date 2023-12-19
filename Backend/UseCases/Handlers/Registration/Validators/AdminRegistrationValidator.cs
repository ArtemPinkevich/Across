using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UseCases.Handlers.Helpers;
using UseCases.Handlers.Registration.Commands;

namespace UseCases.Handlers.Registration.Validators
{
    public class AdminRegistrationValidator : AbstractValidator<AdminRegistrationCommand>
    {
        public AdminRegistrationValidator()
        {
            RuleFor(x => x.SecretPassword).Equal(CrutchConstants.API_REGISTRATION_SECRET_WORD)
                .WithMessage("Неверное секретное слово");
            RuleFor(x => x.Name).NotEmpty().NotNull();
            RuleFor(x => x.Login).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
            RuleFor(x => x.Phone).NotEmpty().NotNull();
        }
    }
}
