using Entities;
using MediatR;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Profiles.Commands;

public class AddOrUpdateDocument: IRequest<ProfileResultDto>
{
    public string UserId { set; get; }
    
    public UserContentType DocumentType { set; get; }
    
    public int DocumentStatus { set; get; }
    
    public string Comment { set; get; }
}