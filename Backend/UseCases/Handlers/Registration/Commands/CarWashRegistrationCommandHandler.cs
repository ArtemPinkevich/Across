namespace UseCases.Handlers.Registration.Commands
{
    using DataAccess.Interfaces;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.Registration.Dto;
    using Entities;
    using System.Collections.Generic;
    using UseCases.Handlers.Helpers;

    public class CarWashRegistrationCommandHandler : IRequestHandler<CarWashRegistrationCommand, CarWashRegistrationResult>
    {
        private readonly IRepository<CarWash> _carWashRepository;
        private readonly IRepository<WorkSchedule> _workScheduleRepository;

        public CarWashRegistrationCommandHandler(IRepository<CarWash> carWashRepository,
                                                 IRepository<WorkSchedule> workScheduleRepository)
        {
            _carWashRepository = carWashRepository;
            _workScheduleRepository = workScheduleRepository;
        }

        public async Task<CarWashRegistrationResult> Handle(CarWashRegistrationCommand request, CancellationToken cancellationToken)
        {
            var carWash = await _carWashRepository.GetAsync(item => item.Name == request.CarWashName);
            if (carWash != null)
                return CreateErrorResult("Автомойка с таким названием уже существует");

            var newCarWash = CreateCarWash(request.CarWashName);
            await _carWashRepository.AddAsync(new List<CarWash>{ newCarWash });
            await _carWashRepository.SaveAsync();
            
            var success = await AddScheduleToCarWash(newCarWash.Id, request.UtcOffset);
            if (!success)
            {
                await _carWashRepository.DeleteAsync(x => x.Id == newCarWash.Id);
                await _carWashRepository.SaveAsync();
                return CreateErrorResult("Ошибка регистрации автомойки");
            }

            await _workScheduleRepository.SaveAsync();
            return CreateSuccessResult("Автомойка зарегистрирована", newCarWash);
        }

        private CarWashRegistrationResult CreateSuccessResult(string reason, CarWash carWash)
        {
            return new CarWashRegistrationResult()
            {
                Result = RegistrationResult.Success,
                Reasons = new string[1] { reason },
                CarWashId = carWash.Id
            };
        }

        private CarWashRegistrationResult CreateErrorResult(string reason)
        {
            return new CarWashRegistrationResult()
            {
                Result = RegistrationResult.Error,
                Reasons = new string[1] { reason },
                CarWashId = null
            };
        }

        private CarWash CreateCarWash(string carWashName)
        {
            return new CarWash
            {
                Name = carWashName,
                Users = new List<User>()
            };
        }

        private async Task<bool> AddScheduleToCarWash(int carWashId, int utcOffset)
        {
            var schedule = await _workScheduleRepository.GetAsync(item => item.CarWashId == carWashId);
            if (schedule != null)
                return false;

            schedule = new WorkSchedule();
            schedule.UtcOffset = utcOffset;
            schedule.CarWashId = carWashId;
            await _workScheduleRepository.AddAsync(new List<WorkSchedule>() { schedule });
            return true;
        }
    }
}
