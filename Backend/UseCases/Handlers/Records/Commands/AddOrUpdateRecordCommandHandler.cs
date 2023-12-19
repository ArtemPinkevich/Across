namespace UseCases.Handlers.Records.Commands
{
    using AutoMapper;
    using DataAccess.Interfaces;
    using Entities;
    using Entities.Enums;
    using MediatR;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.Records.Dto;
    
    using ApplicationServices.Interfaces;

    public class AddOrUpdateRecordCommandHandler : IRequestHandler<AddOrUpdateRecordCommand, RecordDto>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Record> _recordsRepository;
        private readonly IRepository<CarWash> _carWashesRepository;
        private readonly IRepository<WashService> _washServiceRepository;
        private readonly IRepository<CarBody> _carBodyRepository;
        private readonly UserManager<User> _userManager;
        private readonly IFreeTimeSlotService _freeTimeSlotService;

        public AddOrUpdateRecordCommandHandler(IMapper mapper, 
            IRepository<Record> recordsRepository,
            IRepository<CarWash> carWashesRepository,
            IRepository<WashService> washServiceRepository,
            IRepository<CarBody> carBodyRepository,
            UserManager<User> userManager,
            IFreeTimeSlotService freeTimeSlotService)
        {
            _recordsRepository = recordsRepository;
            _carWashesRepository = carWashesRepository;
            _washServiceRepository = washServiceRepository;
            _mapper = mapper;
            _userManager = userManager;
            _carBodyRepository = carBodyRepository;
            _freeTimeSlotService = freeTimeSlotService;
        }

        public async Task<RecordDto> Handle(AddOrUpdateRecordCommand request, CancellationToken cancellationToken)
        {
            var record = request.RecordDto.Id == null
                ? await AddRecord(request.RecordDto, request.CarWashId)
                : await UpdateRecord(request.RecordDto, request.CarWashId);
            await _recordsRepository.SaveAsync();
            var resultRecordDto = _mapper.Map<RecordDto>(record);
            return resultRecordDto;
        }

        private async Task<Record> AddRecord(RecordDto recordDto, int carWashId)
        {
            var record = new Record();
            await UpdateRecordEntity(record, recordDto, carWashId);
            await _recordsRepository.AddAsync(new List<Record>() { record });

            return record;
        }

        private async Task<Record> UpdateRecord(RecordDto recordDto, int carWashId)
        {
            Record record = await _recordsRepository.GetAsync(o => o.Id == recordDto.Id);

            await UpdateRecordEntity(record, recordDto, carWashId);
            await _recordsRepository.UpdateAsync(record);
            return record;
        }


        private async Task UpdateRecordEntity(Record record, RecordDto recordDto, int carWashId)
        {
            CarWash carWash = await _carWashesRepository.GetAsync(o => o.Id == carWashId);
            List<WashService> washServices = await _washServiceRepository.GetAllAsync(o => o.CarWashId == carWashId && 
                                    (o.Id == recordDto.MainServiceId || (recordDto.AdditionServicesIds != null && recordDto.AdditionServicesIds.Contains(o.Id))));

            var carBody = await _carBodyRepository.GetAsync(o => o.Id == recordDto.CarInfo.CarBodyId);

            Vehicle vehicle = new Vehicle()
            {
                RegNumber = recordDto.CarInfo.RegNumber,
                Mark = recordDto.CarInfo.Mark,
                Model = recordDto.CarInfo.Model,
                CarBody = carBody,
            };

            User user = recordDto.PhoneNumber == null 
                ? null 
                : await _userManager.Users.Include(item => item.Vehicles).FirstOrDefaultAsync(item => item.PhoneNumber == recordDto.PhoneNumber);

            if (user != null)
            {
                var userVehicle = user.Vehicles.FirstOrDefault(v => v.RegNumber == recordDto.CarInfo.RegNumber);
                if (userVehicle != null)
                {
                    vehicle = userVehicle;
                }
            }

            int boxId = recordDto.BoxId == 0 ? await GetFreeBoxId(carWashId, recordDto) : recordDto.BoxId;
            if (boxId == 0)
            {
                // TODO Бросить исключение, видимо кто-то успел забронировать выбранное время
            }

            record.CarWashId = carWashId;
            record.CarWash = carWash;
            record.BoxId = boxId;
            record.Date = recordDto.Date;
            record.StartTime = recordDto.StartTime;
            record.TotalDurationMin = recordDto.DurationMin;
            record.TotalPrice = recordDto.Price;
            record.MainWashServiceId = recordDto.MainServiceId;
            record.WashServices = washServices;
            record.Vehicle = vehicle;
            record.PhoneNumber = recordDto.PhoneNumber;
            record.User = user;
            record.Status = RecordStatus.Ok;
        }

        private async Task<int> GetFreeBoxId(int carWashId, RecordDto recordDto)
        {
            CarWash carWash = await _carWashesRepository.GetAsync(o => o.Id == carWashId);

            if (!DateOnly.TryParse(recordDto.Date, out DateOnly recordDate))
            {
                return 0;
            }

            if(!TimeOnly.TryParse(recordDto.StartTime, out TimeOnly recordStartTime))
            {
                return 0;
            }

            TimeOnly recordEndTime = recordStartTime.AddMinutes(recordDto.DurationMin + carWash.ReservedMinutesBetweenRecords);

            // Получаем записи автомойки
            List<Record> allRecords = await _recordsRepository.GetAllAsync(o => o.CarWashId == carWashId && o.Status == RecordStatus.Ok);
            List<Record> filtredRecords = allRecords.Where(o => DateOnly.Parse(o.Date) == recordDate).ToList();

            // Делим записи по боксам
            for (int i = 1; i < carWash.BoxesQuantity + 1; i++)
            {
                var boxRecords = filtredRecords.Where(o => o.BoxId == i).ToList();

                
                if (_freeTimeSlotService.HasBoxFreeTimeSlot(recordStartTime, recordEndTime, boxRecords, carWash.ReservedMinutesBetweenRecords))
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
