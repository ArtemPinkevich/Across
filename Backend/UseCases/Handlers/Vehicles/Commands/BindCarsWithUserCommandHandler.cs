namespace UseCases.Handlers.Vehicles.Commands
{
    using DataAccess.Interfaces;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using Entities;
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using UseCases.Handlers.Records.Dto;

    public class BindCarsWithUserCommandHandler : IRequestHandler<BindCarsWithUserCommand, bool>
    {
        private readonly IRepository<Vehicle> _vehicleRepository;
        private readonly UserManager<User> _userManager;
        private readonly IRepository<CarBody> _carBodyRepository;

        public BindCarsWithUserCommandHandler(UserManager<User> userManager, IRepository<Vehicle> vehicleRepository, IRepository<CarBody> carBodyRepository)
        {
            _vehicleRepository = vehicleRepository;
            _userManager = userManager;
            _carBodyRepository = carBodyRepository;
        }

        public async Task<bool> Handle(BindCarsWithUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userManager.Users.Include(item => item.Vehicles).FirstOrDefaultAsync(item => item.Id == request.UserId);
                if (user != null)
                {
                    // Обновляем или создаем авто в бд в соответствии с прилетевшими
                    await AddOrUpdateVenicle(request.Cars);

                    // Удаляем те авто, которые чисятся за юзером, но нет среди прилетевших
                    var newCarInfoIds = request.Cars.Select(o => o.CreatedId);
                    List<Vehicle> vehiclesForRemove = user.Vehicles.Where(o => !newCarInfoIds.Contains(o.CreatedId)).ToList();
                    vehiclesForRemove.ForEach(o => _vehicleRepository.DeleteAsync(v => v.CreatedId == o.CreatedId));

                    // Связываем список авто с юзером
                    List<Vehicle> vehiclesForBind = await _vehicleRepository.GetAllAsync(o => newCarInfoIds.Contains(o.CreatedId));
                    user.Vehicles = vehiclesForBind;
                    await _userManager.UpdateAsync(user);
                    await _vehicleRepository.SaveAsync();
                }
            }
            catch (Exception exc)
            {
                return false;
            }

            return true;
        }

        private async Task AddOrUpdateVenicle(List<CarInfoDto> cars)
        {
            // Будем накапливать новые авто, чтобы потом сделать добавление в бд одним запросом
            var carsForAdd = new List<CarInfoDto>();
            foreach (var car in cars)
            {
                Vehicle vehicle = await _vehicleRepository.GetAsync(o => o.CreatedId == car.CreatedId);
                if (vehicle == null)
                {
                    carsForAdd.Add(car);
                }
                else
                {
                    var updatedVehicle = await UpdateVeniclePropertiesAsync(vehicle, car);
                    await _vehicleRepository.UpdateAsync(updatedVehicle);
                }
            }

            var vehicleForAdd = new List<Vehicle>();
            foreach (var car in carsForAdd)
            {
                var updatedVehicle = await UpdateVeniclePropertiesAsync(new Vehicle(), car);
                vehicleForAdd.Add(updatedVehicle);
            }

            if (vehicleForAdd.Count > 0)
            {
                await _vehicleRepository.AddAsync(vehicleForAdd);
            }
        }

        private async Task<Vehicle> UpdateVeniclePropertiesAsync(Vehicle vehicle, CarInfoDto carInfo)
        {
            var carBody = await _carBodyRepository.GetAsync(o => o.Id == carInfo.CarBodyId);

            vehicle.RegNumber = carInfo.RegNumber;
            vehicle.Mark = carInfo.Mark;
            vehicle.Model = carInfo.Model;
            vehicle.CarBody = carBody;
            vehicle.CreatedId = carInfo.CreatedId;

            return vehicle;
        }
    }
}
