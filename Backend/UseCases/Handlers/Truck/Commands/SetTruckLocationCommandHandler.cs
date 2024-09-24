using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using MediatR;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Commands;

public class SetTruckLocationCommandHandler:IRequestHandler<SetTruckLocationCommand, TruckResultDto>
{
    private readonly IRepository<Entities.Truck> _truckRepository;

    public SetTruckLocationCommandHandler(IRepository<Entities.Truck> truckRepository)
    {
        _truckRepository = truckRepository;
    }
    
    public async Task<TruckResultDto> Handle(SetTruckLocationCommand request, CancellationToken cancellationToken)
    {
        var truck = await _truckRepository.GetAsync(x => x.Id == request.TruckId);
        if (truck == null)
        {
            return new TruckResultDto()
            {
                Result = ApiResult.Failed, Reasons = new[] { $"no truck found with id {request.TruckId}" }
            };
        }

        truck.TruckLocation = request.TruckLocation;
        truck.Latitude = request.Latitude;
        truck.Longitude = request.Longitude;
        await _truckRepository.UpdateAsync(truck);
        await _truckRepository.SaveAsync();

        return new TruckResultDto() { Result = ApiResult.Success };
    }
}