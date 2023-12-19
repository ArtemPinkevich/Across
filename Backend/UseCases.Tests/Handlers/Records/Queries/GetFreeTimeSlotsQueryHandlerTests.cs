using ApplicationServices.Implementation;

namespace UseCases.Tests.Handlers.Records.Queries
{
    using DataAccess.Interfaces;
    using Entities;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.Records.Dto;
    using UseCases.Handlers.Records.Queries;

    [TestClass()]
    public class GetFreeTimeSlotsQueryHandlerTests
    {
        // Не заданы параметры рабочего времени автомойки
        // Ожидаем получить пустой список свободных тайм-поинтов
        [TestMethod()]
        public async Task HandleReturnEmptyTestAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>());

            CarWash carWashFake = new CarWash()
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 1,
                WorkSchedule = new WorkSchedule()
                {
                    SundayBegin = "",   // Симуляция того, будто не заданы параметры рабочего времени автомойки
                    SundayEnd = ""
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);


            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate = "2021.10.17";      // воскресенье
            var durationMin = 70;
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());

            var expectedTimeStamps = new List<string>();

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }

        // Записей нет
        // Продолжительность мойки 70мин
        // Ожидаем получить все тайм-поинты, т.е. от 12:00 до 13:50 (за 70мин до конца раб.времени) с шагом 10 мин
        [TestMethod()]
        public async Task HandleReturnSlotsForAllDayTestAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>());

            CarWash carWashFake = new CarWash() 
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 2,
                WorkSchedule = new WorkSchedule()
                {
                    MondayBegin = "12:00",   // 10:00
                    MondayEnd = "15:00",     // 15:00
                    SundayBegin = "12:00",   // 10:00
                    SundayEnd = "15:00"      // 15:00
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);


            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate = "2021.10.17";
            var durationMin = 70;
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());

            var expectedTimeStamps = new List<string>()
            {
                "12:00",
                "12:10",
                "12:20",
                "12:30",
                "12:40",
                "12:50",
                "13:00",
                "13:10",
                "13:20",
                "13:30",
                "13:40",
                "13:50",
            };

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }

        // Есть запись с 11:00 до 14:00
        // Продолжительность мойки 40мин
        // Ожидаем получить тайм-поинты от 10:00 до 10:20 (11:00 - 40мин) и от 14:00 до 14:20 (за 40 мин до конца раб.дня)
        [TestMethod()]
        public async Task HandleReturnWithoutBusySlotsTestAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            var recordFake = new Record()
            {
                Date = "2021.10.17",
                StartTime = "11:00",
                TotalDurationMin = 180,
                BoxId = 1
            };
            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>() { recordFake });

            CarWash carWashFake = new CarWash()
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 1,
                WorkSchedule = new WorkSchedule()
                {
                    MondayBegin = "10:00",
                    MondayEnd = "15:00",
                    SundayBegin = "10:00",
                    SundayEnd = "15:00"
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);


            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate = "2021.10.17";      // воскресенье
            var durationMin = 40;
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());

            var expectedTimeStamps = new List<string>()
            {
                "10:00",
                "10:10",
                "10:20",
                "14:00",
                "14:10",
                "14:20",
            };

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }

        // Есть только 1 свободный тайм-слот в 60 мин
        // Ожидаем получить 1 свободный тайм-поинт
        [TestMethod()]
        public async Task HandleReturnOnlyOneSlotTestAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            var recordFake = new Record()
            {
                Date = "2021.10.17",    // воскресенье
                StartTime = "10:00",
                TotalDurationMin = 180,
                BoxId = 1
            };

            var recordFake2 = new Record()
            {
                Date = "2021.10.17",    // воскресенье
                StartTime = "14:00",
                TotalDurationMin = 50,
                BoxId = 1
            };

            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>() { recordFake, recordFake2 });

            CarWash carWashFake = new CarWash()
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 1,
                WorkSchedule = new WorkSchedule()
                {
                    MondayBegin = "10:00",
                    MondayEnd = "15:00",
                    SundayBegin = "10:00",
                    SundayEnd = "15:00"
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);

            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate = "2021.10.17";      // воскресенье
            var durationMin = 60;
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());
            //List<string> actuallyTimeStamps = GetTimeStamps(timeSlotsDto.FreeTimePoints);

            var expectedTimeStamps = new List<string>()
            {
                "13:00",
            };

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }

        // Есть только 1 свободный тайм-слот в 60 мин в 1 боксе,
        // но 2-й бокс полностью свободен
        // Ожидаем получить все тайм-поинты, т.е. от 12:00 до 14:00 (за 60мин до конца раб.времени) с шагом 10 мин
        [TestMethod()]
        public async Task HandleReturnSlotForAllDayBox2TestAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            var recordFake = new Record()
            {
                Date = "2021.10.17",    // воскресенье
                StartTime = "12:00",
                TotalDurationMin = 60,
                BoxId = 1
            };

            var recordFake2 = new Record()
            {
                Date = "2021.10.17",    // воскресенье
                StartTime = "14:00",
                TotalDurationMin = 50,
                BoxId = 1
            };
            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>() { recordFake, recordFake2 });

            CarWash carWashFake = new CarWash()
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 2,
                WorkSchedule = new WorkSchedule()
                {
                    MondayBegin = "12:00",
                    MondayEnd = "15:00",
                    SundayBegin = "12:00",
                    SundayEnd = "15:00"
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);


            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate = "2021.10.17";      // воскресенье
            var durationMin = 60;
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());

            var expectedTimeStamps = new List<string>()
            {
                "12:00",
                "12:10",
                "12:20",
                "12:30",
                "12:40",
                "12:50",
                "13:00",
                "13:10",
                "13:20",
                "13:30",
                "13:40",
                "13:50",
                "14:00",
            };

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }

        // В двух боксах есть по 1 свободному тайм-слоту в 70 мин 
        // Ожидаем получить 2 тайм-поинта
        [TestMethod()]
        public async Task HandleReturn2PointsFrom2BoxesTestAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            var recordFake = new Record()
            {
                Date = "2021.10.17",    // воскресенье
                StartTime = "10:00",
                TotalDurationMin = 230,
                BoxId = 1
            };

            var recordFake2 = new Record()
            {
                Date = "2021.10.17",    // воскресенье
                StartTime = "11:10",
                TotalDurationMin = 240,
                BoxId = 2
            };
            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>() { recordFake, recordFake2 });

            CarWash carWashFake = new CarWash()
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 2,
                WorkSchedule = new WorkSchedule()
                {
                    MondayBegin = "10:00",
                    MondayEnd = "15:00",
                    SundayBegin = "10:00",
                    SundayEnd = "15:00"
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);


            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate = "2021.10.17";      // воскресенье
            var durationMin = 70;
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());

            var expectedTimeStamps = new List<string>()
            {
                "10:00",
                "13:50",
            };

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }

        // Сегодня всё занято, а завтра все свободно,
        // Делается запрос на завтра
        // Ожидаем получить все тайм-поинты, т.е. от 10:00 до 13:50 (за 70мин до конца раб.времени) с шагом 10 мин
        [TestMethod()]
        public async Task HandleReturnAllSlotsForTomorrowTestAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            var recordFake = new Record()
            {
                Date = "2021.10.17",    // воскресенье
                StartTime = "10:00",
                TotalDurationMin = 300,
                BoxId = 1
            };

            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>() { recordFake });

            CarWash carWashFake = new CarWash()
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 1,
                WorkSchedule = new WorkSchedule()
                {
                    MondayBegin = "10:00",
                    MondayEnd = "14:00",
                    SundayBegin = "10:00",
                    SundayEnd = "15:00"
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);


            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate = "2021.10.11";      // понедельник
            var durationMin = 70;
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());

            var expectedTimeStamps = new List<string>()
            {
                "10:00",
                "10:10",
                "10:20",
                "10:30",
                "10:40",
                "10:50",
                "11:00",
                "11:10",
                "11:20",
                "11:30",
                "11:40",
                "11:50",
                "12:00",
                "12:10",
                "12:20",
                "12:30",
                "12:40",
                "12:50",
            };

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }

        // Завтра всё занято, а сегодня всё свободно,
        // Делается запрос на сегодня
        // Ожидаем получить все тайм-поинты, т.е. от 10:00 до 13:50 (за 70мин до конца раб.времени) с шагом 10 мин
        [TestMethod()]
        public async Task HandleReturnAllSlotsForTodayTestAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            var recordFake = new Record()
            {
                Date = "2021.10.18",
                StartTime = "10:00",
                TotalDurationMin = 300,
                BoxId = 1
            };

            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>() { recordFake });

            CarWash carWashFake = new CarWash()
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 1,
                WorkSchedule = new WorkSchedule()
                {
                    MondayBegin = "10:00",
                    MondayEnd = "15:00",
                    SundayBegin = "10:00",
                    SundayEnd = "15:00"
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);


            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate = "2021.10.17";      // воскресенье
            var durationMin = 70;
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());

            var expectedTimeStamps = new List<string>()
            {
                "10:00",
                "10:10",
                "10:20",
                "10:30",
                "10:40",
                "10:50",
                "11:00",
                "11:10",
                "11:20",
                "11:30",
                "11:40",
                "11:50",
                "12:00",
                "12:10",
                "12:20",
                "12:30",
                "12:40",
                "12:50",
                "13:00",
                "13:10",
                "13:20",
                "13:30",
                "13:40",
                "13:50",
            };

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }

        // Проверка, что цикл не уйдет в бесконечность в случае когда Duration меньше timeStep (шаг, с которым мы движемся по времени в поисках свободного слота)
        [TestMethod(), Timeout(2000)]
        public async Task HandleNotFailedForShortDurationAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>());

            CarWash carWashFake = new CarWash()
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 1,
                WorkSchedule = new WorkSchedule()
                {
                    SundayBegin = "23:00",
                    SundayEnd = "00:00"
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);

            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate = "2021.10.17";    // воскресенье
            var durationMin = 9;                // меньше timeStep = 10
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());

            var expectedTimeStamps = new List<string>()
            {
                "23:00",
                "23:10",
                "23:20",
                "23:30",
                "23:40",
                "23:50",
            };

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }

        // Тоже самое, что и предыдущий тест (HandleNotFailedForShortDurationAsync), но теперь Duration равен timeStep (сейчас он 10 мин)
        [TestMethod(), Timeout(2000)]
        public async Task HandleNotFailedForDurationSameStepAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>());

            CarWash carWashFake = new CarWash()
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 1,
                WorkSchedule = new WorkSchedule()
                {
                    SundayBegin = "23:00",
                    SundayEnd = "00:00"
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);

            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate = "2021.10.17";      // воскресенье
            var durationMin = 10;
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());

            var expectedTimeStamps = new List<string>()
            {
                "23:00",
                "23:10",
                "23:20",
                "23:30",
                "23:40",
                "23:50",
            };

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }

        // Запрос свободных тайм-поинтов на завтрашний день
        // Записей нет
        // Продолжительность мойки 70мин
        // Ожидаем получить все тайм-поинты, ВАЖНО дата должна совпадать с запрашиваемой
        // UPDATED! Данный тест надо переписать, так как теперь тайм слоты возвращаются без даты
        [TestMethod()]
        public async Task HandleReturnSlotsForTomorrowTestAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>());

            CarWash carWashFake = new CarWash()
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 2,
                WorkSchedule = new WorkSchedule()
                {
                    MondayBegin = "12:00",
                    MondayEnd = "15:00",
                    SundayBegin = "12:00",
                    SundayEnd = "15:00",
                    TuesdayBegin = "12:00",
                    TuesdayEnd = "15:00",
                    WednesdayBegin = "12:00",
                    WednesdayEnd = "15:00",
                    ThursdayBegin = "12:00",
                    ThursdayEnd = "15:00",
                    FridayBegin = "12:00",
                    FridayEnd = "15:00",
                    SaturdayBegin = "12:00",
                    SaturdayEnd = "15:00",
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);

            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate =  DateTime.Today.AddDays(1).ToString("yyyy.MM.dd");      // Завтра
            var durationMin = 70;
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());

            var expectedTimeStamps = new List<string>()
            {
                "12:00",
                "12:10",
                "12:20",
                "12:30",
                "12:40",
                "12:50",
                "13:00",
                "13:10",
                "13:20",
                "13:30",
                "13:40",
                "13:50",
            };

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }

        // Есть запись с 13:00 на 30 минут
        // Резерв 30 мин (объект тестирования)
        // Продолжительность мойки 30мин
        // Ожидаем получить тайм-поинты:
            // 12:00, т.к. запрос 30 мин помещается между 12 и 13 с учетом резерва в 30 мин
            // 14:00-14:30, т.к. в конце дня резерв не требуется
        [TestMethod()]
        public async Task HandleReturnSlotsWithReservedMinBetweenRecordsTestAsync()
        {
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var _carWashRepositoryMock = Substitute.For<IRepository<CarWash>>();
            var _freetimeSlotService = new FreeTimeSlotService();

            var recordFake = new Record()
            {
                Date = "2021.10.17",
                StartTime = "13:00",
                TotalDurationMin = 30,
                BoxId = 1
            };
            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>() { recordFake });


            CarWash carWashFake = new CarWash()
            {
                Name = "Fake Car Wash",
                BoxesQuantity = 1,
                ReservedMinutesBetweenRecords = 30,
                WorkSchedule = new WorkSchedule()
                {
                    MondayBegin = "12:00",   // 10:00
                    MondayEnd = "15:00",     // 15:00
                    SundayBegin = "12:00",   // 10:00
                    SundayEnd = "15:00"      // 15:00
                }
            };
            _carWashRepositoryMock.GetAsync(default).ReturnsForAnyArgs(carWashFake);


            var getFreeTimeSlotsQueryHandler = new GetFreeTimeSlotsQueryHandler(_recordRepositoryMock, _carWashRepositoryMock, _freetimeSlotService);

            var carWashId = 1;
            var selectedDate = "2021.10.17";
            var durationMin = 30;
            var getFreeTimeSlotsQueryFake = new GetFreeTimeSlotsQuery(carWashId, selectedDate, durationMin);

            FreeTimePointsResponceDto timeSlotsDto = await getFreeTimeSlotsQueryHandler.Handle(getFreeTimeSlotsQueryFake, new CancellationToken());

            var expectedTimeStamps = new List<string>()
            {
                "12:00",
                "14:00",
                "14:10",
                "14:20",
                "14:30",
            };

            Assert.IsTrue(timeSlotsDto.FreeTimePoints.SequenceEqual(expectedTimeStamps));
        }
    }
}
