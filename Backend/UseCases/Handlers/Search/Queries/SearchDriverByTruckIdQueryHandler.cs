using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Profiles.Dto;
using UseCases.Handlers.Profiles.Helpers;

namespace UseCases.Handlers.Search.Queries;

public class SearchDriverByTruckIdQueryHandler : IRequestHandler<SearchDriverByTruckIdQuery, ProfileDto>
{
    private readonly IRepository<Entities.Truck> _truckRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public SearchDriverByTruckIdQueryHandler(IRepository<Entities.Truck> truckRepository,
        UserManager<User> userManager,
        IMapper mapper)
    {
        _truckRepository = truckRepository;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<ProfileDto> Handle(SearchDriverByTruckIdQuery request, CancellationToken cancellationToken)
    {
        var truck = await _truckRepository.GetAsync(x => x.Id == request.TruckId);
        if (truck == null)
        {
            throw new Exception($"no truck found with id {request.TruckId}");
        }

        var driver = truck.Driver;
        var role = await _userManager.GetUserRole(driver);
        if (role != UserRoles.Driver)
        {
            throw new Exception($"user id not driver {request.TruckId}");
        }

        return await driver.ConvertToProfileDtoAsync(_userManager, _mapper);
    }
}