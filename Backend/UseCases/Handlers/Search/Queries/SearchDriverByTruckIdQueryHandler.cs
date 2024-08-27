using System;
using System.Threading;
using System.Threading.Tasks;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Common.Extensions;
using UseCases.Handlers.Profiles.Dto;

namespace UseCases.Handlers.Search.Queries;

public class SearchDriverByTruckIdQueryHandler : IRequestHandler<SearchDriverByTruckIdQuery, ProfileDto>
{
    private readonly IRepository<Entities.Truck> _truckRepository;
    private readonly UserManager<User> _userManager;

    public SearchDriverByTruckIdQueryHandler(IRepository<Entities.Truck> truckRepository, UserManager<User> userManager)
    {
        _truckRepository = truckRepository;
        _userManager = userManager;
    }
    
    public async Task<ProfileDto> Handle(SearchDriverByTruckIdQuery request, CancellationToken cancellationToken)
    {
        var truck = await _truckRepository.GetAsync(x => x.Id == request.TruckId);
        if (truck == null)
        {
            throw new Exception($"no truck found with id {request.TruckId}");
        }

        var user = truck.User;
        var role = await _userManager.GetUserRole(user);
        if (role != UserRoles.Driver)
        {
            throw new Exception($"user id not driver {request.TruckId}");
        }
        
        return new ProfileDto()
        {
            Name = user.Name,
            Surname = user.Surname,
            Patronymic = user.Patronymic,
            BirthDate = user.BirthDate,
            PhoneNumber = user.PhoneNumber,
            Status = user.UserStatus,
            Role = role,
            DocumentDtos = null
        };
    }
}