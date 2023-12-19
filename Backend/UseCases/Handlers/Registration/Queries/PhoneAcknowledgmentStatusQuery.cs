namespace UseCases.Handlers.Registration.Queries
{
    using MediatR;

    public class PhoneAcknowledgmentStatusQuery : IRequest<bool>
    {
        public string PhoneNumber { get; }

        public PhoneAcknowledgmentStatusQuery(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
    }
}
