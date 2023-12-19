namespace UseCases.Exceptions
{
    using System;

    public class NotAuthorizedException: Exception
    {
        public string AuthMessage { set; get; }
    }
}
