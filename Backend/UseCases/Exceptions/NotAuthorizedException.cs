namespace UseCases.Exceptions
{
    using System;
    
    public class NotAuthorizedException: Exception
    {
        public NotAuthorizedErrorCode ErrorCode { set; get; }
        
        public string AuthorizationMessage { set; get; }
    }
}
