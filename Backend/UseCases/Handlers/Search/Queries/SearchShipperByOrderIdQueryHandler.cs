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

namespace UseCases.Handlers.Search.Queries;

public class SearchShipperByOrderIdQueryHandler : IRequestHandler<SearchShipperByOrderIdQuery, ProfileDto>
{
    private readonly IRepository<Entities.TransportationOrder> _transportationOrdersRepository;
    private readonly UserManager<User> _userManager;

    public SearchShipperByOrderIdQueryHandler (IRepository<Entities.TransportationOrder> transportationOrdersRepository,
        UserManager<User> userManager)
    {
        _transportationOrdersRepository = transportationOrdersRepository;
        _userManager = userManager;
    }
        
    public async Task<ProfileDto> Handle(SearchShipperByOrderIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _transportationOrdersRepository.GetAsync(x => x.Id == request.OrderId);
        if (order == null)
        {
            throw new Exception($"no orders found with id {request.OrderId}");
        }

        var user = order.Shipper;

        var role = await _userManager.GetUserRole(user);
        if (role != UserRoles.Shipper)
        {
            throw new Exception($"user is not shipper {request.OrderId}");
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