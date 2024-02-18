using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UseCases.Handlers.Cargo.Dto;

namespace UseCases.Handlers.Cargo.Commands;

public class AddOrUpdateCargoCommandHandler: IRequestHandler<AddOrUpdateCargoCommand, CargoResult>
{
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IRepository<Entities.Cargo> _repository;

    public AddOrUpdateCargoCommandHandler(UserManager<User> userManager,
        IRepository<Entities.Cargo> repository,
        IMapper mapper)
    {
        _userManager = userManager;
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<CargoResult> Handle(AddOrUpdateCargoCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
        {
            return new CargoResult()
            {
                Result = Result.Error,
                Reasons = new[] { "no user found" }
            };
        }

        if (request.CargoDto.Id == null)
        {
            var cargo = _mapper.Map<Entities.Cargo>(request.CargoDto);
            cargo.UserId = user.Id;
            await _repository.AddAsync(new List<Entities.Cargo>() {cargo});
        }
        else
        {
            var cargo = await _repository.GetAsync(x => x.Id == request.CargoDto.Id);
            if (cargo == null)
            {
                return new CargoResult()
                {
                    Result = Result.Error,
                    Reasons = new[] { $"no cargo found with Id={request.CargoDto.Id}" }
                };
            }

            cargo.CreatedId = request.CargoDto.CreatedId;
            cargo.Name = request.CargoDto.Name;
            cargo.Weight = request.CargoDto.Weight;
            cargo.Volume = request.CargoDto.Volume;
            cargo.PackagingType = request.CargoDto.PackagingType;
            cargo.PackagingQuantity = request.CargoDto.PackagingQuantity;
            cargo.Length = request.CargoDto.Length;
            cargo.Width = request.CargoDto.Width;
            cargo.Height = request.CargoDto.Height;
            cargo.Diameter = request.CargoDto.Diameter;
            cargo.UserId = user.Id;

            await _repository.UpdateAsync(cargo);
        }

        await _repository.SaveAsync();

        return new CargoResult()
        {
            Result = Result.Ok,
        };
    }
}