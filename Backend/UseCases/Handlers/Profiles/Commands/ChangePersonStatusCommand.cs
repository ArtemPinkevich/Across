using MediatR;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Commands;

public class ChangePersonStatusCommand: IRequest<ProfileResultDto>
{
    public string UserId { set; get; }
        
    public int PersonStatus { set; get; }

    public string Comment { set; get; }
}