using Entities;

namespace UseCases.Handlers.Profiles.Dto;

public class DocumentDto
{
    public DocumentStatus DocumentStatus { set; get; }
    
    public DocumentType DocumentType { set; get; }
    
    public string Comment { set; get; }
}