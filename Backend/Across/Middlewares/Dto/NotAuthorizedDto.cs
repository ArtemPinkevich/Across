using UseCases.Exceptions;

namespace Across.Middlewares.Dto;

public class NotAuthorizedDto
{
    public NotAuthorizedErrorCode ErrorCode { set; get; }
        
    public string AuthorizationMessage { set; get; }
    
    public string ExceptionMessage { set; get; }
}