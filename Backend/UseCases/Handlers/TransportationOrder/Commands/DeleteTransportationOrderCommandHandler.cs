using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class DeleteTransportationOrderCommandHandler:IRequestHandler<DeleteTransportationOrderCommand, TransportationOrderResult>
{
    private readonly IRepository<Entities.TransportationOrder> _repository;

    public DeleteTransportationOrderCommandHandler(IRepository<Entities.TransportationOrder> repository)
    {
        _repository = repository;
    }
    
    public async Task<TransportationOrderResult> Handle(DeleteTransportationOrderCommand request, CancellationToken cancellationToken)
    {
        var cargo = await _repository.GetAsync(x => x.Id == request.TransportationOrderId);
        if (cargo == null)
        {
            return new TransportationOrderResult()
            {
                Result = Result.Error,
                Reasons = new[] { $"no cargo found with id = {request.TransportationOrderId}" }
            };
        }

        await _repository.DeleteAsync(x => x.Id == request.TransportationOrderId);
        await _repository.SaveAsync();

        return new TransportationOrderResult()
        {
            Result = Result.Ok,
        };
    }
}