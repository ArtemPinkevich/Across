namespace Entities;

public class Document: EntityBase
{
    public string UserId { get; set; }
    public User User { get; set; }
    
    public DocumentType DocumentType { get; set; }
    
    public DocumentStatus DocumentStatus { get; set; }
    
    public string Comment { get; set; }
}