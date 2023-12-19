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
    public class CarWashRegistrationValidator: AbstractValidator<CarWashRegistrationCommand>
    {
        public CarWashRegistrationValidator()
        {
            RuleFor(x => x.SecretPassword).Equal(CrutchConstants.API_REGISTRATION_SECRET_WORD)
                .WithMessage("Неверное секретное слово");
            RuleFor(x => x.CarWashName).NotNull().NotEmpty();
            RuleFor(x => x.UtcOffset).GreaterThanOrEqualTo(0);
        }
    }
}
