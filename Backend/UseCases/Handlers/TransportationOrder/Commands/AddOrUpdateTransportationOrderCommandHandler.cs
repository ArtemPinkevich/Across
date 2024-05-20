using System;
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
                Result = ApiResult.Failed,
                Reasons = new[] { "no user found" }
            };
        }

        Entities.TransportationOrder order;
        if (request.TransportationOrderDto.TransportationOrderId == null)
        {
            order = _mapper.Map<Entities.TransportationOrder>(request.TransportationOrderDto);
            order.UserId = user.Id;
            order.TransferChangeHistoryRecords = new List<TransferChangeStatusRecord>()
            {
                new ()
                {
                    ChangeDatetime = DateTime.Now,
                    TransportationOrderId = order.Id,
                    TransportationStatus = TransportationStatus.CarrierFinding
                }
            };
            await _repository.AddAsync(new List<Entities.TransportationOrder>() {order});
        }
        else
        {
            order = await _repository.GetAsync(x => x.Id == request.TransportationOrderDto.TransportationOrderId);
            if (order == null)
            {
                return new TransportationOrderResult()
                {
                    Result = ApiResult.Failed,
                    Reasons = new[] { $"no cargo found with Id={request.TransportationOrderDto.TransportationOrderId}" }
                };
            }

            order.LoadDateFrom = request.TransportationOrderDto.TransferInfo.LoadingDateFrom;
            order.LoadDateTo = request.TransportationOrderDto.TransferInfo.loadingDateTo;
            order.LoadingLocalityName = request.TransportationOrderDto.TransferInfo.LoadingLocalityName;
            order.LoadingAddress = request.TransportationOrderDto.TransferInfo.LoadingAddress;
            order.UnloadingLocalityName = request.TransportationOrderDto.TransferInfo.UnloadingLocalityName;
            order.UnloadingAddress = request.TransportationOrderDto.TransferInfo.UnloadingAddress;

            //думаю лучше использовать второй вариант, где каждое свойство обновляется отдельно
            //в первом варианте прежняя запись в бд удаляется и создается новая
#if true
            order.Cargo = _mapper.Map<Entities.Cargo>(request.TransportationOrderDto.Cargo);
            order.TruckRequirements =
                _mapper.Map<TruckRequirements>(request.TransportationOrderDto.Cargo.TruckRequirements);
#else
            
            order.Cargo.Diameter = request.TransportationOrderDto.Load.Diameter;
            order.Cargo.Height = request.TransportationOrderDto.Load.Height;
            order.Cargo.Length = request.TransportationOrderDto.Load.Length;
            order.Cargo.Name = request.TransportationOrderDto.Load.Name;
            order.Cargo.Volume = request.TransportationOrderDto.Load.Volume;
            order.Cargo.Weight = request.TransportationOrderDto.Load.Weight;
            order.Cargo.Width = request.TransportationOrderDto.Load.Width;
            order.Cargo.CreatedId = request.TransportationOrderDto.Load.CreatedId;
            order.Cargo.PackagingQuantity = request.TransportationOrderDto.Load.PackagingQuantity;
            order.Cargo.PackagingType = request.TransportationOrderDto.Load.PackagingType;

            order.TruckRequirements.Adr1 = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.Adr1;
            order.TruckRequirements.Adr2 = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.Adr2;
            order.TruckRequirements.Adr3 = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.Adr3;
            order.TruckRequirements.Adr4 = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.Adr4;
            order.TruckRequirements.Adr5 = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.Adr5;
            order.TruckRequirements.Adr6 = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.Adr6;
            order.TruckRequirements.Adr7 = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.Adr7;
            order.TruckRequirements.Adr8 = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.Adr8;
            order.TruckRequirements.Adr9 = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.Adr9;
            order.TruckRequirements.Ekmt = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.Ekmt;
            order.TruckRequirements.Tir = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.Tir;
            order.TruckRequirements.CarryingCapacity =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarryingCapacity;
            order.TruckRequirements.HasLiftGate = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.HasLiftGate;
            order.TruckRequirements.HasLTtl = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.HasLTtl;
            order.TruckRequirements.HasStanchionTrailer = request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.HasStanchionTrailer;
            order.TruckRequirements.LoadingType = _mapper.Map<LoadingType>(request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.LoadingTypeDtos);
            order.TruckRequirements.UnloadingType = _mapper.Map<LoadingType>(request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.UnloadingTypeDtos);
            
            order.TruckRequirements.CarBodyRequirement.Adjustable =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Adjustable);
            order.TruckRequirements.CarBodyRequirement.Autocart =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Autocart);
            order.TruckRequirements.CarBodyRequirement.Autotower =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Autotower);
            order.TruckRequirements.CarBodyRequirement.Barge =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Barge);
            order.TruckRequirements.CarBodyRequirement.Bus =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Bus);
            order.TruckRequirements.CarBodyRequirement.Cattle =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Cattle);
            order.TruckRequirements.CarBodyRequirement.Container =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Container);
            order.TruckRequirements.CarBodyRequirement.Crane =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Crane);
            order.TruckRequirements.CarBodyRequirement.Dolly =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Dolly);
            order.TruckRequirements.CarBodyRequirement.Doppelstock =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Doppelstock);
            order.TruckRequirements.CarBodyRequirement.Flatbed =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Flatbed);
            order.TruckRequirements.CarBodyRequirement.Gas =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Gas);
            order.TruckRequirements.CarBodyRequirement.Innloader =
                request.TransportationOrderDto.Load.TruckRequirementsForLoadDto.CarBodies.Any(x =>
                    x == CarBodyType.Innloader);
            
#endif
            
            await _repository.UpdateAsync(order);
        }

        await _repository.SaveAsync();

        return new TransportationOrderResult()
        {
            TransportationId = order.Id,
            Result = ApiResult.Success,
        };
    }
}