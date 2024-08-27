using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UseCases.Handlers.Cargo.Dto;
using UseCases.Handlers.Common.Dto;
using UseCases.Handlers.Common.Extensions;

namespace UseCases.Handlers.TransportationOrder.Queries;

public class GetTransportationOrdersQueryHandler: IRequestHandler<GetTransportationOrdersQuery, TransportationOrdersListDto>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Entities.TransportationOrder> _ordersRepository;
    private readonly IRepository<TransferAssignedTruckRecord> _ordersAssignedTruckRepository;
    private readonly IRepository<Entities.Truck> _truckRepository;
    private readonly UserManager<User> _userManager;

    public GetTransportationOrdersQueryHandler(UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> ordersRepository,
        IRepository<TransferAssignedTruckRecord> ordersAssignedTruckRepository,
        IRepository<Entities.Truck> truckRepository,
        IMapper mapper)
    {
        _userManager = userManager;
        _ordersRepository = ordersRepository;
        _ordersAssignedTruckRepository = ordersAssignedTruckRepository;
        _truckRepository = truckRepository;
        _mapper = mapper;
    }
    
    public async Task<TransportationOrdersListDto> Handle(GetTransportationOrdersQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return CreateNoUserFoundError(request);

        var role = await _userManager.GetUserRole(user);

        if (role == UserRoles.Driver)
            return await GetDriverOrders(user);
        
        if (role == UserRoles.Shipper)
            return await GetShipperOrders(user);

        return CreateUndefinedRoleError(request, role);
    }

    private async Task<TransportationOrdersListDto> GetDriverOrders(User user)
    {
        var userWithTrucks = await _userManager.Users.Include(x => x.Trucks).FirstOrDefaultAsync(x => x.Id == user.Id);
        if (userWithTrucks == null)
        {
            return new TransportationOrdersListDto()
            {
                Result = new TransportationOrderResult()
                {
                    Result = ApiResult.Failed
                }
            };  
        }

        List<TransportationOrderDto> transportationOrderDtos = new List<TransportationOrderDto>();
        //find all records where truck of driver was assigned
        foreach (var truck in userWithTrucks.Trucks)
        {
            var assignedTruckRecords = await _ordersAssignedTruckRepository.GetAllAsync(x => x.TruckId == truck.Id);
            foreach (TransferAssignedTruckRecord record in assignedTruckRecords)
            {
                Entities.TransportationOrder order = await _ordersRepository.GetAsync(x => x.Id == record.TransportationOrderId);
                var orderDto = _mapper.Map<TransportationOrderDto>(order);
                transportationOrderDtos.Add(orderDto);
            }

            var orders = await _ordersRepository.GetAllAsync(x => x.Trucks.Any(o => o.Id == truck.Id));
            transportationOrderDtos.AddRange(orders.Select(o => _mapper.Map<TransportationOrderDto>(o)));
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

    private async Task<TransportationOrdersListDto> GetShipperOrders(User user)
    {
        var transportationOrders = await _ordersRepository.GetAllAsync(x => x.UserId == user.Id);

        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult()
            {
                Result = ApiResult.Success,
            },
            TransportationOrderDtos = transportationOrders.Select(x => _mapper.Map<TransportationOrderDto>(x)).ToList()
        };
    }

    private TransportationOrdersListDto CreateNoUserFoundError(GetTransportationOrdersQuery request)
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
    
    private TransportationOrdersListDto CreateUndefinedRoleError(GetTransportationOrdersQuery request, string role)
    {
        return new TransportationOrdersListDto()
        {
            Result = new TransportationOrderResult()
            {
                Result = ApiResult.Failed,
                Reasons = new string[] { $"user role is not Driver or Shipper role:{role} userId:{request.UserId}" }
            }
        };
    }
}