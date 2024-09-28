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
using UseCases.Handlers.TransportationOrder.Mapping;

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
            order.ShipperId = user.Id;
            order.TransportationOrderStatus = TransportationOrderStatus.CarrierFinding;
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

            //думаю лучше использовать второй вариант, где каждое свойство обновляется отдельно
            //в первом варианте прежняя запись в бд удаляется и создается новая
#if false
            order.Cargo = _mapper.Map<Entities.Cargo>(request.TransportationOrderDto.Cargo);
            order.TruckRequirements =
                _mapper.Map<TruckRequirements>(request.TransportationOrderDto.Cargo.TruckRequirements);
            order.TransportationInfo = _mapper.Map<TransportationInfo>(request.TransportationOrderDto.TransferInfo);
#else
            
            order.Cargo.Diameter = request.TransportationOrderDto.Cargo.Diameter;
            order.Cargo.Height = request.TransportationOrderDto.Cargo.Height;
            order.Cargo.Length = request.TransportationOrderDto.Cargo.Length;
            order.Cargo.Name = request.TransportationOrderDto.Cargo.Name;
            order.Cargo.Volume = request.TransportationOrderDto.Cargo.Volume;
            order.Cargo.Weight = request.TransportationOrderDto.Cargo.Weight;
            order.Cargo.Width = request.TransportationOrderDto.Cargo.Width;
            order.Cargo.CreatedId = request.TransportationOrderDto.Cargo.CreatedId;
            order.Cargo.PackagingQuantity = request.TransportationOrderDto.Cargo.PackagingQuantity;
            order.Cargo.PackagingType = request.TransportationOrderDto.Cargo.PackagingType;

            order.TruckRequirements.Adr1 = request.TransportationOrderDto.Cargo.TruckRequirements.Adr1;
            order.TruckRequirements.Adr2 = request.TransportationOrderDto.Cargo.TruckRequirements.Adr2;
            order.TruckRequirements.Adr3 = request.TransportationOrderDto.Cargo.TruckRequirements.Adr3;
            order.TruckRequirements.Adr4 = request.TransportationOrderDto.Cargo.TruckRequirements.Adr4;
            order.TruckRequirements.Adr5 = request.TransportationOrderDto.Cargo.TruckRequirements.Adr5;
            order.TruckRequirements.Adr6 = request.TransportationOrderDto.Cargo.TruckRequirements.Adr6;
            order.TruckRequirements.Adr7 = request.TransportationOrderDto.Cargo.TruckRequirements.Adr7;
            order.TruckRequirements.Adr8 = request.TransportationOrderDto.Cargo.TruckRequirements.Adr8;
            order.TruckRequirements.Adr9 = request.TransportationOrderDto.Cargo.TruckRequirements.Adr9;
            order.TruckRequirements.Ekmt = request.TransportationOrderDto.Cargo.TruckRequirements.Ekmt;
            order.TruckRequirements.Tir = request.TransportationOrderDto.Cargo.TruckRequirements.Tir;
            order.TruckRequirements.CarryingCapacity =
                request.TransportationOrderDto.Cargo.TruckRequirements.CarryingCapacity;
            order.TruckRequirements.BodyVolume = request.TransportationOrderDto.Cargo.TruckRequirements.BodyVolume;
            order.TruckRequirements.InnerBodyHeight = request.TransportationOrderDto.Cargo.TruckRequirements.InnerBodyHeight;
            order.TruckRequirements.InnerBodyLength = request.TransportationOrderDto.Cargo.TruckRequirements.InnerBodyLength;
            order.TruckRequirements.InnerBodyWidth = request.TransportationOrderDto.Cargo.TruckRequirements.InnerBodyWidth;
            order.TruckRequirements.HasLiftgate = request.TransportationOrderDto.Cargo.TruckRequirements.HasLiftgate;
            order.TruckRequirements.HasLtl = request.TransportationOrderDto.Cargo.TruckRequirements.HasLtl;
            order.TruckRequirements.HasStanchionTrailer = request.TransportationOrderDto.Cargo.TruckRequirements.HasStanchionTrailer;
            order.TruckRequirements.LoadingType = _mapper.Map<LoadingType>(request.TransportationOrderDto.Cargo.TruckRequirements.LoadingTypeDtos);
            order.TruckRequirements.UnloadingType = _mapper.Map<LoadingType>(request.TransportationOrderDto.Cargo.TruckRequirements.UnloadingTypeDtos);
            order.TruckRequirements.CarBodyRequirement = _mapper.Map<CarBodyRequirement>(request.TransportationOrderDto.Cargo.TruckRequirements.CarBodies);
            
            order.TransportationInfo.LoadDateFrom = request.TransportationOrderDto.TransferInfo.LoadingDateFrom;
            order.TransportationInfo.LoadDateTo = request.TransportationOrderDto.TransferInfo.LoadingDateTo;
            order.TransportationInfo.LoadingLocalityName = _mapper.Map<string>(request.TransportationOrderDto.TransferInfo.LoadingPlace);
            order.TransportationInfo.LoadingAddress = request.TransportationOrderDto.TransferInfo.LoadingAddress;
            order.TransportationInfo.LoadingLatitude = request.TransportationOrderDto.TransferInfo.LoadingPlace?.Latitide ?? "";
            order.TransportationInfo.LoadingLongitude = request.TransportationOrderDto.TransferInfo.LoadingPlace?.Longtitude ?? "";
            order.TransportationInfo.UnloadingLocalityName =_mapper.Map<string>(request.TransportationOrderDto.TransferInfo.UnloadingPlace);
            order.TransportationInfo.UnloadingAddress = request.TransportationOrderDto.TransferInfo.UnloadingAddress;
            order.TransportationInfo.UnloadingLatitude = request.TransportationOrderDto.TransferInfo.UnloadingPlace?.Latitide ?? "";
            order.TransportationInfo.UnloadingLongitude = request.TransportationOrderDto.TransferInfo.UnloadingPlace?.Longtitude ?? "";
            
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