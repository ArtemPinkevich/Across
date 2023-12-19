namespace UseCases.Handlers.Registration.Dto
{
    public class SendVerificationCodeResult
    {
        public bool Success { set; get; }

        public string[] Reasons { set; get; }
    }
}
