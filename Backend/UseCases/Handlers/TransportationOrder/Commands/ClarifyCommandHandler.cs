using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class ClarifyCommandHandler:IRequestHandler<ClarifyCommand, TransportationOrderResult>
{
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;

    public ClarifyCommandHandler(IRepository<Entities.TransportationOrder> ordersRepository)
    {
        _ordersRepository = ordersRepository;
    }
    
    public async Task<TransportationOrderResult> Handle(ClarifyCommand request, CancellationToken cancellationToken)
    {
        var order = await _ordersRepository.GetAsync(x => x.Id == request.TransportationOrderId);
        if (order == null)
        {
            return new TransportationOrderResult()
            {
                TransportationId = request.TransportationOrderId,
                Result = ApiResult.Failed,
                Reasons = new[] { $"no order found with id {request.TransportationOrderId}" }
            };
        }

        order.ContactInformation ??= new ContactInformation();

        order.ContactInformation.LoadingTime = request.LoadingTime;
        order.ContactInformation.LoadingContactPerson = request.LoadingContactPerson;
        order.ContactInformation.LoadingContactPhone = request.LoadingContactPhone;
        order.ContactInformation.UnloadingContactPerson = request.UnloadingContactPerson;
        order.ContactInformation.UnloadingContactPhone = request.UnloadingContactPhone;

        await _ordersRepository.UpdateAsync(order);
        await _ordersRepository.SaveAsync();

        return new TransportationOrderResult() { TransportationId = order.Id, Result = ApiResult.Success, };
    }
}