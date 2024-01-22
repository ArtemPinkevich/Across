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
    public class CarWashRegistrationValidator: AbstractValidator<DriverRegistrationCommand>
    {
        public CarWashRegistrationValidator()
        {
            
        }
    }
}
