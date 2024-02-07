using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Commands;

public class AddTruckToUserCommandHandler : IRequestHandler<AddTruckToUserCommand, TruckResultDto>
{
    private readonly IRepository<Entities.Truck> _repository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    
    public AddTruckToUserCommandHandler(IRepository<Entities.Truck> repository,
        UserManager<User> userManager,
        IMapper mapper)
    {
        _repository = repository;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<TruckResultDto> Handle(AddTruckToUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return CreateNoUserFoundResult();
        }

        Entities.Truck truck = _mapper.Map<Entities.Truck>(request.TruckDto);
        truck.UserId = user.Id;
        await _repository.AddAsync(new List<Entities.Truck>() { truck });
        
        return new TruckResultDto()
        {
            Result = TruckResult.Ok,
        };
    }

    private TruckResultDto CreateNoUserFoundResult()
    {
        return new TruckResultDto()
        {
            Result = TruckResult.Ok,
            Reasons = new[] { "No user found" }
        };
    }
}