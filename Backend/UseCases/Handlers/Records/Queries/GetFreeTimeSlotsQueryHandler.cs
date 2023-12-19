namespace UseCases.Handlers.Records.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DataAccess.Interfaces;
    using Entities;
    using MediatR;
    using UseCases.Handlers.Records.Dto;
    using ApplicationServices.Interfaces;
    using Entities.Enums;


    public class GetFreeTimeSlotsQueryHandler : IRequestHandler<GetFreeTimeSlotsQuery, FreeTimePointsResponceDto>
    {
        private readonly IRepository<Record> _recordsRepository;
        private readonly IRepository<CarWash> _carWashesRepository;
        private readonly IFreeTimeSlotService _freeTimeSlotService;

        public GetFreeTimeSlotsQueryHandler(IRepository<Record> recordsRepository,
            IRepository<CarWash> carWashesRepository,
            IFreeTimeSlotService freeTimeSlotService)
        {
            _recordsRepository = recordsRepository;
            _carWashesRepository = carWashesRepository;
            _freeTimeSlotService = freeTimeSlotService;
        }

        public async Task<FreeTimePointsResponceDto> Handle(GetFreeTimeSlotsQuery request, CancellationToken cancellationToken)
        {
            var timeSlotsDto = new FreeTimePointsResponceDto() { FreeTimePoints = new List<string>() };

            if (!DateOnly.TryParseExact(request.SelectedDate, "yyyy.MM.dd", out DateOnly dateFromRequest))
            {
                return timeSlotsDto;
            }

            CarWash carWash = await _carWashesRepository.GetAsync(o => o.Id == request.CarWashId);

            if (carWash == null || carWash.WorkSchedule == null)
            {
                return timeSlotsDto;
            }

            // Сколько сейчас времени в часовом поясе автомойки
            DateTime carWashDateTimeNow = DateTime.UtcNow.AddHours(carWash.WorkSchedule.UtcOffset);
            TimeOnly carWashTimeNow = TimeOnly.FromDateTime(carWashDateTimeNow);

            // Если запрос пришел из вчера, то возвращаем пустой массив.
            // Такое возможно, если попытаться забронить из часового пояса ближе к UTC в конце дня, когда в автомойке уже наступил новый день
            // Внимание!! Для Unit-тестов! Перед запуском нужно комментить это условие или в тестах ставить дату НЕ "из прошлого"
            if (dateFromRequest < DateOnly.FromDateTime(carWashDateTimeNow.Date))
            {
                return timeSlotsDto;
            }

            // Получаем записи автомойки
            List<Record> allRecords = await _recordsRepository.GetAllAsync(o => o.CarWashId == request.CarWashId && o.Status == RecordStatus.Ok);
            List<Record> filtredRecords = allRecords.Where(o => DateOnly.Parse(o.Date) == dateFromRequest).ToList();

            // Делим записи по боксам
            Dictionary<int, List<Record>> boxRecords = new Dictionary<int, List<Record>>();
            for (int i = 1; i < carWash.BoxesQuantity + 1; i++)
            {
                boxRecords.Add(i, filtredRecords.Where(o => o.BoxId == i).ToList());
            }

            // Получаем начало рабочего дня, по которому идет запрос
            string rawBeginTimeOWorkDay = GetBeginTimeByWeekday(carWash, dateFromRequest);
            if (!TimeOnly.TryParse(rawBeginTimeOWorkDay, out TimeOnly timeOfWorkDayBegin))
            {
                return timeSlotsDto;
            }

            // Получаем конец рабочего дня, по которому идет запрос, как DateTime
            string rawEndTimeOWorkDay = GetEndTimeByWeekday(carWash, dateFromRequest);
            if (!TimeOnly.TryParse(rawEndTimeOWorkDay, out TimeOnly timeOfWorkDayEnd))
            {
                return timeSlotsDto;
            }

            // Вычисляем первый и последний тайм-поинты
            TimeOnly currentTimeSlotBeginTime = GetFirstTimePoint(dateFromRequest, carWashTimeNow, timeOfWorkDayBegin);
            TimeOnly lastTimePointOfWorkDay = timeOfWorkDayEnd.AddMinutes(-request.DurationMin);

            // Делаем цикл от первого тайм-поинта до последнего
            // и на каждом шаге проверяем на наличие пересечений с существующими записями
            int stepOfTimePointInMinutes = 10;
            HashSet<TimeOnly> freeTimePoints = new HashSet<TimeOnly>();
            while (currentTimeSlotBeginTime <= lastTimePointOfWorkDay)
            {
                var currentTimeSlotEndTime = currentTimeSlotBeginTime.AddMinutes(request.DurationMin + carWash.ReservedMinutesBetweenRecords);
                foreach (List<Record> boxRecordsList in boxRecords.Values)
                {
                    if (_freeTimeSlotService.HasBoxFreeTimeSlot(currentTimeSlotBeginTime, currentTimeSlotEndTime, boxRecordsList, carWash.ReservedMinutesBetweenRecords))
                    {
                        freeTimePoints.Add(currentTimeSlotBeginTime);
                        break;
                    }
                }

                // Проверка на перевал за 00:00
                // Код добавлен по мотивам бага, когда DurationMin меньше чем stepOfTimePointInMinutes
                // в таком случае, без данной проверки, возможно что цикл подет так ...23:40, 23:50, 00:00, и начнется поновой и будет бесконечным.
                if ((lastTimePointOfWorkDay - currentTimeSlotBeginTime).TotalMinutes < stepOfTimePointInMinutes)
                {
                    break;
                }

                currentTimeSlotBeginTime = currentTimeSlotBeginTime.AddMinutes(stepOfTimePointInMinutes);
            }

            var freeTimePointsAsStrings = freeTimePoints.Select(o => o.ToString("t", CultureInfo.GetCultureInfo("ru-RU"))).ToList();
            timeSlotsDto.FreeTimePoints = freeTimePointsAsStrings;

            return timeSlotsDto;
        }

        // Если не текущий день, то берем начало дня, если текущий - либо начало рабочего дня, либо текущее время округленное до круглого числа в большую сторону
        private TimeOnly GetFirstTimePoint(DateOnly requestedDay, TimeOnly carWashTimeNow, TimeOnly beginWorkTime)
        {
            if (requestedDay != DateOnly.FromDateTime(DateTime.Today) || carWashTimeNow < beginWorkTime)
            {
                return beginWorkTime;
            }

            // Округляем до кратного 10
            var span = new TimeSpan(0, 10, 0);
            long ticks = (carWashTimeNow.Ticks + span.Ticks - 1) / span.Ticks;
            var result = new TimeOnly(ticks * span.Ticks);

            return result;
        }

        private string GetBeginTimeByWeekday(CarWash carWash, DateOnly date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday: return carWash.WorkSchedule.MondayBegin;
                case DayOfWeek.Tuesday: return carWash.WorkSchedule.TuesdayBegin;
                case DayOfWeek.Wednesday: return carWash.WorkSchedule.WednesdayBegin;
                case DayOfWeek.Thursday: return carWash.WorkSchedule.ThursdayBegin;
                case DayOfWeek.Friday: return carWash.WorkSchedule.FridayBegin;
                case DayOfWeek.Saturday: return carWash.WorkSchedule.SaturdayBegin;
                case DayOfWeek.Sunday: return carWash.WorkSchedule.SundayBegin;
                default: throw new ArgumentException("Недопустимый код операции");
            }
        }

        private string GetEndTimeByWeekday(CarWash carWash, DateOnly date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday: return carWash.WorkSchedule.MondayEnd;
                case DayOfWeek.Tuesday: return carWash.WorkSchedule.TuesdayEnd;
                case DayOfWeek.Wednesday: return carWash.WorkSchedule.WednesdayEnd;
                case DayOfWeek.Thursday: return carWash.WorkSchedule.ThursdayEnd;
                case DayOfWeek.Friday: return carWash.WorkSchedule.FridayEnd;
                case DayOfWeek.Saturday: return carWash.WorkSchedule.SaturdayEnd;
                case DayOfWeek.Sunday: return carWash.WorkSchedule.SundayEnd;
                default: throw new ArgumentException("Недопустимый код операции");
            }
        }
    }
}
