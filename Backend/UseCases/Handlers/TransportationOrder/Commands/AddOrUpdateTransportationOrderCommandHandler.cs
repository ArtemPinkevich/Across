using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.TransportationOrder.Commands;

public class AddOrUpdateTransportationOrderCommandHandler: IRequestHandler<AddOrUpdateTransportationOrderCommand, TransportationOrderResult>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Entities.TransportationOrder> _repository;

    public AddOrUpdateTransportationOrderCommandHandler(UserManager<User> userManager,
        IRepository<Entities.TransportationOrder> repository,
        IMapper mapper)
    {
        _userManager = userManager;
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<TransportationOrderResult> Handle(AddOrUpdateTransportationOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return new TransportationOrderResult()
            {
                Result = Result.Error,
                Reasons = new[] { "no user found" }
            };
        }

        Entities.TransportationOrder order;
        if (request.TransportationOrderDto.TransportationOrderId == null)
        {
            order = _mapper.Map<Entities.TransportationOrder>(request.TransportationOrderDto);
            order.UserId = user.Id;
            await _repository.AddAsync(new List<Entities.TransportationOrder>() {order});
        }
        else
        {
            order = await _repository.GetAsync(x => x.Id == request.TransportationOrderDto.TransportationOrderId);
            if (order == null)
            {
                return new TransportationOrderResult()
                {
                    Result = Result.Error,
                    Reasons = new[] { $"no cargo found with Id={request.TransportationOrderDto.TransportationOrderId}" }
                };
            }

            await _repository.UpdateAsync(order);
        }

        await _repository.SaveAsync();

        return new TransportationOrderResult()
        {
            TransportationId = order.Id,
            Result = Result.Ok,
        };
    }
}