using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Queries;

public class GetQueriesCommandHandler:IRequestHandler<GetTrucksQuery, TrucksListResultDto>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager; 
    private readonly IRepository<Entities.Truck> _repository;

    public GetQueriesCommandHandler(IRepository<Entities.Truck> repository, UserManager<User> userManager,IMapper mapper)
    {
        _repository = repository;
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<TrucksListResultDto> Handle(GetTrucksQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return new TrucksListResultDto()
            {
                Result = new TruckResultDto()
                {
                    Result = TruckResult.Error,
                    Reasons = new []{"no user found"}
                }
            };
        }

        var trucks = await _repository.GetAllAsync(x => x.UserId == request.UserId);
        return new TrucksListResultDto()
        {
            Result = new TruckResultDto()
            {
                Result = TruckResult.Ok,
            },
            Trucks = trucks.Select(x => _mapper.Map<TruckDto>(x)).ToList(),
        };
    }
}