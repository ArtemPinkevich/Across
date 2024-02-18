using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using MediatR;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.Cargo.Commands;

public class DeleteCargoCommandHandler:IRequestHandler<DeleteCargoCommand, CargoResult>
{
    private readonly IRepository<Entities.Cargo> _repository;

    public DeleteCargoCommandHandler(IRepository<Entities.Cargo> repository)
    {
        _repository = repository;
    }
    
    public async Task<CargoResult> Handle(DeleteCargoCommand request, CancellationToken cancellationToken)
    {
        var cargo = await _repository.GetAsync(x => x.Id == request.CargoId);
        if (cargo == null)
        {
            return new CargoResult()
            {
                Result = Result.Error,
                Reasons = new[] { $"no cargo found with id = {request.CargoId}" }
            };
        }

        await _repository.DeleteAsync(x => x.Id == request.CargoId);
        await _repository.SaveAsync();

        return new CargoResult()
        {
            Result = Result.Ok,
        };
    }
}