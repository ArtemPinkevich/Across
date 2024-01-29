using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Commands;

public class AddTruckToUserCommandHandler : IRequestHandler<AddTruckToUserCommand, TruckResultDto>
{
    public AddTruckToUserCommandHandler()
    {
        
    }
    
    public Task<TruckResultDto> Handle(AddTruckToUserCommand request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new TruckResultDto()
        {
            Result = TruckResult.Ok,
        });
    }
}