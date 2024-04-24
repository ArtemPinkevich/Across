using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using UseCases.Handlers.Authorization.Queries;

namespace UseCases.Handlers.Authorization.Validators
{
    public class AuthorizationQueryValidator: AbstractValidator<AuthorizationQuery>
    {
        public AuthorizationQueryValidator()
        {
            RuleFor(x => x.Phone).NotNull().NotEmpty();
        }
    }
}
