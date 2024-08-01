using MediatR;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Commands;

public class ChangeDocumentStatusCommand: IRequest<ProfileResultDto>
{
    public string UserId { set; get; }
    
    public int DocumentType { set; get; }
    
    public int DocumentStatus { set; get; }
}