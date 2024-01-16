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

        public CarWashRegistrationCommandHandler(IRepository<CarWash> carWashRepository)
        {
            _carWashRepository = carWashRepository;
        }

        public async Task<CarWashRegistrationResult> Handle(CarWashRegistrationCommand request, CancellationToken cancellationToken)
        {
            var carWash = await _carWashRepository.GetAsync(item => item.Name == request.CarWashName);
            if (carWash != null)
                return CreateErrorResult("Автомойка с таким названием уже существует");

            var newCarWash = CreateCarWash(request.CarWashName);
            await _carWashRepository.AddAsync(new List<CarWash>{ newCarWash });
            await _carWashRepository.SaveAsync();
            
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
    }
}
