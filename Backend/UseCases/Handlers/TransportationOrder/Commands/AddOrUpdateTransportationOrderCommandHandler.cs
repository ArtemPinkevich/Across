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
    private readonly IRepository<Entities.Cargo> _repository;

    public AddOrUpdateTransportationOrderCommandHandler(UserManager<User> userManager,
        IRepository<Entities.Cargo> repository,
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

        if (request.TransportationOrderDto.Id == null)
        {
            var cargo = _mapper.Map<Entities.Cargo>(request.TransportationOrderDto);
            cargo.UserId = user.Id;
            await _repository.AddAsync(new List<Entities.Cargo>() {cargo});
        }
        else
        {
            var cargo = await _repository.GetAsync(x => x.Id == request.TransportationOrderDto.Id);
            if (cargo == null)
            {
                return new TransportationOrderResult()
                {
                    Result = Result.Error,
                    Reasons = new[] { $"no cargo found with Id={request.TransportationOrderDto.Id}" }
                };
            }

            cargo.CreatedId = request.TransportationOrderDto.CreatedId;
            cargo.Name = request.TransportationOrderDto.Name;
            cargo.Weight = request.TransportationOrderDto.Weight;
            cargo.Volume = request.TransportationOrderDto.Volume;
            cargo.PackagingType = request.TransportationOrderDto.PackagingType;
            cargo.PackagingQuantity = request.TransportationOrderDto.PackagingQuantity;
            cargo.Length = request.TransportationOrderDto.Length;
            cargo.Width = request.TransportationOrderDto.Width;
            cargo.Height = request.TransportationOrderDto.Height;
            cargo.Diameter = request.TransportationOrderDto.Diameter;
            cargo.UserId = user.Id;

            await _repository.UpdateAsync(cargo);
        }

        await _repository.SaveAsync();

        return new TransportationOrderResult()
        {
            Result = Result.Ok,
        };
    }
}