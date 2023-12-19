namespace UseCases.Handlers.Authorization
{
    public class AuthorizationDto
    {
        public int Id { set; get; }

        public string AccessToken { set; get; }

        public string RefreshToken { set; get; }

        public string ExpireDateTime { set; get; }
    }
}
