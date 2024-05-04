using Entities;
using FluentValidation;
using UseCases.Handlers.Profiles.Commands;

namespace UseCases.Handlers.Profiles.Validators;

public class ChangeRoleCommandValidator: AbstractValidator<ChangeRoleCommand>
{
    public ChangeRoleCommandValidator()
    {
        RuleFor(x => x.Role)
            .NotEmpty()
            .NotNull()
            .Must(IsRoleValid);
    }

    private bool IsRoleValid(string role)
    {
        return role is UserRoles.Admin or UserRoles.Driver or UserRoles.Shipper or UserRoles.Owner;
    }
}