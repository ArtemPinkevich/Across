using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Common.Extensions;

namespace UseCases.Handlers.TransportationOrder.Queries;

public class GetOrdersHistoryQueryHandler : IRequestHandler<GetOrdersHistoryQuery, TransportationOrdersListDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<Transportation> _transportationRepository;
    private readonly UserManager<User> _userManager;
    
    public GetOrdersHistoryQueryHandler(UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<Transportation> transportationRepository,
        IMapper mapper)
    {
        _userManager = userManager;
        _ordersRepository = ordersRepository;
        _transportationRepository = transportationRepository;
        _mapper = mapper;
    }
    
    public async Task<TransportationOrdersListDto> Handle(GetOrdersHistoryQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return CreateNoUserFoundError(request);
        
        List<TransportationOrderDto> transportationOrderDtos = new List<TransportationOrderDto>();

        var role = await _userManager.GetUserRole(user);
        if (role == UserRoles.Driver)
        {
            var transportations = await _transportationRepository.GetAllAsync(x => x.DriverId == user.Id);
            foreach (var transportation in transportations)
            {
                var order = await _ordersRepository.GetAsync(x => x.Id == transportation.TransportationOrderId && x.TransportationOrderStatus == TransportationOrderStatus.Done);
                if (order != null)
                {
                    transportationOrderDtos.Add(_mapper.Map<TransportationOrderDto>(order));
                }
            }
        }

        if (role == UserRoles.Shipper)
        {
            var order = await _ordersRepository.GetAsync(x => x.ShipperId == user.Id && x.TransportationOrderStatus == TransportationOrderStatus.Done);
            if (order != null)
            {
                transportationOrderDtos.Add(_mapper.Map<TransportationOrderDto>(order));
            }
        }

        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult()
            {
                Result = ApiResult.Success,
            },
            TransportationOrderDtos = transportationOrderDtos
        };
    }
    
    private TransportationOrdersListDto CreateNoUserFoundError(GetOrdersHistoryQuery request)
    {
        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult()
            {
                Result = ApiResult.Failed,
                Reasons = new string[] { $"no user found with id {request.UserId}" }
            }
        };
    }
}