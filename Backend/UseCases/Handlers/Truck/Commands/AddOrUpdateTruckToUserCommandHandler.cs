﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Truck.Dto;

namespace UseCases.Handlers.Truck.Commands;

public class AddOrUpdateTruckToUserCommandHandler : IRequestHandler<AddOrUpdateTruckToUserCommand, TruckResultDto>
{
    private readonly IRepository<Entities.Truck> _repository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    
    public AddOrUpdateTruckToUserCommandHandler(IRepository<Entities.Truck> repository,
        UserManager<User> userManager,
        IMapper mapper)
    {
        _repository = repository;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    public async Task<TruckResultDto> Handle(AddOrUpdateTruckToUserCommand request, CancellationToken cancellationToken)
    {
        User user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return CreateNoUserFoundResult();
        }

        if (request.TruckDto.TruckId == null)
        {
            Entities.Truck truck = _mapper.Map<Entities.Truck>(request.TruckDto);
            truck.UserId = user.Id;
            await _repository.AddAsync(new List<Entities.Truck>() { truck });
        }
        else
        {
            var truck = await _repository.GetAsync(x => x.Id == request.TruckDto.TruckId);
            if (truck == null)
            {
                return CreateNoTruckFoundResult(request.TruckDto.TruckId);
            }

            truck.createdId = request.TruckDto.createdId;
            truck.regNumber = request.TruckDto.regNumber;
            truck.TrailerType = request.TruckDto.TrailerType;
            truck.CarBodyType = request.TruckDto.CarBodyType;
            truck.LoadingType = _mapper.Map<LoadingType>(request.TruckDto.LoadingType);
            truck.HasLTtl = request.TruckDto.HasLTtl;
            truck.HasLiftGate = request.TruckDto.HasLiftGate;
            truck.HasStanchionTrailer = request.TruckDto.HasStanchionTrailer;
            truck.BodyVolume = request.TruckDto.BodyVolume;
            truck.InnerBodyLength = request.TruckDto.InnerBodyLength;
            truck.InnerBodyWidth = request.TruckDto.InnerBodyWidth;
            truck.InnerBodyHeight = request.TruckDto.InnerBodyHeight;
            truck.Adr1 = request.TruckDto.Adr1;
            truck.Adr2 = request.TruckDto.Adr2;
            truck.Adr3 = request.TruckDto.Adr3;
            truck.Adr4 = request.TruckDto.Adr4;
            truck.Adr5 = request.TruckDto.Adr5;
            truck.Adr6 = request.TruckDto.Adr6;
            truck.Adr7 = request.TruckDto.Adr7;
            truck.Adr8 = request.TruckDto.Adr8;
            truck.Adr9 = request.TruckDto.Adr9;
            truck.Tir = request.TruckDto.Tir;
            truck.Ekmt = request.TruckDto.Ekmt;
            truck.UserId = user.Id;
            
            await _repository.UpdateAsync(truck);
        }

        await _repository.SaveAsync();

        
        return new TruckResultDto()
        {
            Result = TruckResult.Ok,
        };
    }
    
    private TruckResultDto CreateNoUserFoundResult()
    {
        return new TruckResultDto()
        {
            Result = TruckResult.Error,
            Reasons = new[] { "No user found" }
        };
    }

    private TruckResultDto CreateNoTruckFoundResult(int? truckId)
    {
        return new TruckResultDto()
        {
            Result = TruckResult.Error,
            Reasons = new[] { $"no truck with id={truckId} was found" },
        };
    }
}