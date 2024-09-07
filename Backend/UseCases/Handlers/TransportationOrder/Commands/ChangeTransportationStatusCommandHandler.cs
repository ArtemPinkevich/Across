using DataAccess.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class ChangeTransportationStatusCommandHandler : IRequestHandler<ChangeTransportationStatusCommand, TransportationOrderResult>
{
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;

    public ChangeTransportationStatusCommandHandler(IRepository<Entities.TransportationOrder> ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }

    public async Task<TransportationOrderResult> Handle(ChangeTransportationStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetAsync(o => o.Id == request.TransportationOrderId);
        order.TransportationOrderStatus = request.TransportationStatus;

        await _ordersRepository.SaveAsync();

        return new TransportationOrderResult() { Result = ApiResult.Success };
    }
}
