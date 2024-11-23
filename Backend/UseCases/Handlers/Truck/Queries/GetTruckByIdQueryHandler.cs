using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Queries;

public class GetTruckByIdQueryHandler : IRequestHandler<GetTruckByIdQuery, TruckDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.Truck> _repository;

    public GetTruckByIdQueryHandler(IRepository<Entities.Truck> trucksRepository, IMapper mapper)
    {
        _mapper = mapper;
        _repository = trucksRepository;
    }

    public async Task<TruckDto> Handle(GetTruckByIdQuery request, CancellationToken cancellationToken)
    {
        Entities.Truck truck = await _repository.GetAsync(x => x.Id == request.TruckId && x.IsActive);

        var truckDto = _mapper.Map<TruckDto>(truck);

        return truckDto;
    }
}
