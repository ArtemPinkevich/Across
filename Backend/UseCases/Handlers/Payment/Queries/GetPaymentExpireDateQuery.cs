using MediatR;
using UseCases.Handlers.Payment.Dto;

namespace UseCases.Handlers.Payment.Queries
{
    public class GetPaymentExpireDateQuery : IRequest<PaymentExpireDateDto>
    {
        public string UserId { set; get; }
    }
}