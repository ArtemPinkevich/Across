namespace UseCases.Handlers.CarWash.Queries
{
    using DataAccess.Interfaces;
    using MediatR;
    using AutoMapper;
    using System.Threading;
    using System.Threading.Tasks;
    using Dto;
    using Entities;
    using System;

    public class UpdateCarWashSettingsCommandHandler : IRequestHandler<UpdateCarWashSettingsCommand, CarWashDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<CarWash> _carWashesRepository;

        public UpdateCarWashSettingsCommandHandler(IMapper mapper,
            IRepository<CarWash> carWashesRepository)
        {
            _mapper = mapper;
            _carWashesRepository = carWashesRepository;
        }

        public async Task<CarWashDto> Handle(UpdateCarWashSettingsCommand request, CancellationToken cancellationToken)
        {
            var carWash = await UpdateCarWash(request.CarWashDto);
            await _carWashesRepository.SaveAsync();
            
            return _mapper.Map<CarWashDto>(carWash);
        }

        private async Task<CarWash> UpdateCarWash(CarWashDto carWashDto)
        {
            CarWash carWash = await _carWashesRepository.GetAsync(o => o.Id == carWashDto.Id);

            if (carWash == null)
            {
                throw new Exception("Автомойка с заданным id не найдена");
            }

            carWash.Name = carWashDto.Name;
            carWash.Phone = carWashDto.Phone;
            carWash.BoxesQuantity = carWashDto.BoxesQuantity;
            carWash.ReservedMinutesBetweenRecords = carWashDto.ReservedMinutesBetweenRecords;
            carWash.Location = carWashDto.Location;

            carWash.WorkSchedule ??= new WorkSchedule();
            carWash.WorkSchedule.MondayBegin = carWashDto.WorkTime?.MondayBegin;
            carWash.WorkSchedule.MondayEnd= carWashDto.WorkTime?.MondayEnd;
            carWash.WorkSchedule.TuesdayBegin = carWashDto.WorkTime?.TuesdayBegin;
            carWash.WorkSchedule.TuesdayEnd = carWashDto.WorkTime?.TuesdayEnd;
            carWash.WorkSchedule.WednesdayBegin = carWashDto.WorkTime?.WednesdayBegin;
            carWash.WorkSchedule.WednesdayEnd = carWashDto.WorkTime?.WednesdayEnd;
            carWash.WorkSchedule.ThursdayBegin = carWashDto.WorkTime?.ThursdayBegin;
            carWash.WorkSchedule.ThursdayEnd = carWashDto.WorkTime?.ThursdayEnd;
            carWash.WorkSchedule.FridayBegin = carWashDto.WorkTime?.FridayBegin;
            carWash.WorkSchedule.FridayEnd = carWashDto.WorkTime?.FridayEnd;
            carWash.WorkSchedule.SaturdayBegin = carWashDto.WorkTime?.SaturdayBegin;
            carWash.WorkSchedule.SaturdayEnd = carWashDto.WorkTime?.SaturdayEnd;
            carWash.WorkSchedule.SundayBegin = carWashDto.WorkTime?.SundayBegin;
            carWash.WorkSchedule.SundayEnd = carWashDto.WorkTime?.SundayEnd;
            
            await _carWashesRepository.UpdateAsync(carWash);
            return carWash;
        }
    }
}
