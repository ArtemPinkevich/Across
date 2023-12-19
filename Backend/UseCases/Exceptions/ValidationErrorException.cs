using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace UseCases.Exceptions
{
    public class ValidationErrorException: ValidationException
    {
        public ValidationErrorException(List<FluentValidation.Results.ValidationFailure> results)
            : base(results)
        {

        }
    }
}
