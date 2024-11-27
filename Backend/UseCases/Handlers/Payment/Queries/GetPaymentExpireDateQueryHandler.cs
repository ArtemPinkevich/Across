using MediatR;
using System.Threading.Tasks;
using System.Threading;
using UseCases.Handlers.Payment.Dto;
using DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace UseCases.Handlers.Payment.Queries;

public class GetPaymentExpireDateQueryHandler : IRequestHandler<GetPaymentExpireDateQuery, PaymentExpireDateDto>
{
    private readonly IRepository<Entities.Payment> _paymentRepository;

    public GetPaymentExpireDateQueryHandler(IRepository<Entities.Payment> paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentExpireDateDto> Handle(GetPaymentExpireDateQuery request, CancellationToken cancellationToken)
    {
        List<Entities.Payment> userPayments = await _paymentRepository.GetAllAsync(o => o.UserId == request.UserId);
        var userPayment = userPayments.LastOrDefault();

        var paymentExpireDateDto = new PaymentExpireDateDto();
        paymentExpireDateDto.PaymentExpireDate = userPayment?.PaymentExpireDate ?? string.Empty;


        return paymentExpireDateDto;
    }
}
