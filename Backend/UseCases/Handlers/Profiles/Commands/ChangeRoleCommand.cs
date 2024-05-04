using MediatR;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Commands;

public class ChangeRoleCommand: IRequest<ChangeRoleResultDto>
{
    public string UserId { set; get; }
    public string Role { set; get; }
}