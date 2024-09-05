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
    private readonly IMapper _mapper;

    public SearchShipperByOrderIdQueryHandler (IRepository<Entities.TransportationOrder> transportationOrdersRepository,
        UserManager<User> userManager,
        IMapper mapper)
    {
        _transportationOrdersRepository = transportationOrdersRepository;
        _userManager = userManager;
        _mapper = mapper;
    }
        
    public async Task<ProfileDto> Handle(SearchShipperByOrderIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _transportationOrdersRepository.GetAsync(x => x.Id == request.OrderId);
        if (order == null)
        {
            throw new Exception($"no orders found with id {request.OrderId}");
        }

        var shipper = await _userManager.FindByIdWithDocuments(order.ShipperId);

        var role = await _userManager.GetUserRole(shipper);
        if (role != UserRoles.Shipper)
        {
            throw new Exception($"user is not shipper {request.OrderId}");
        }

        return await shipper.ConvertToProfileDto(_userManager, _mapper);
    }
}