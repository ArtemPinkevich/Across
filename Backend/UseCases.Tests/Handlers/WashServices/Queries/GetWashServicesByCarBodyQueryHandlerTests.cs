using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataAccess.Interfaces;
using Entities;
using Moq;
using NUnit.Framework;
using UseCases.Handlers.WashServices.Dto;
using UseCases.Handlers.WashServices.Mappings;
using UseCases.Handlers.WashServices.Queries;

namespace UseCases.Tests.Handlers.WashServices.Queries
{
    [TestFixture]
    public class GetWashServicesByCarBodyQueryHandlerTests
    {
        private static PriceGroup PriceGroup0 = new PriceGroup()
        {
            Id = 0,
            Name = "PriceGroup0",
            CarBodies = new List<CarBody>()
            {
                new CarBody() { Id = 0, CarBodyName = "Седан" },
                new CarBody() { Id = 1, CarBodyName = "Хэтчбек" },
            }
        };

        private static readonly PriceGroup PriceGroup1 = new PriceGroup()
        {
            Id = 1,
            Name = "PriceGroup1",
            CarBodies = new List<CarBody>()
            {
                new CarBody() { Id = 2, CarBodyName = "Универсал" },
                new CarBody() { Id = 3, CarBodyName = "Внедорожник" },
                new CarBody() { Id = 4, CarBodyName = "Кроссовер" },
            }
        };

        private static readonly PriceGroup PriceGroup2 = new PriceGroup()
        {
            Id = 2,
            Name = "PriceGroup2",
            CarBodies = new List<CarBody>()
            {
                new CarBody() { Id = 5, CarBodyName = "Пикап" },
                new CarBody() { Id = 6, CarBodyName = "Фургон" },
                new CarBody() { Id = 7, CarBodyName = "Лимузин" },
                new CarBody() { Id = 8, CarBodyName = "Минивэн" },
            }
        };

        private static readonly PriceGroup PriceGroup3 = new PriceGroup()
        {
            Id = 3,
            Name = "PriceGroup3",
            CarBodies = new List<CarBody>()
            {
                new CarBody() { Id = 9, CarBodyName = "Автобус" },
            }
        };

        private readonly WashService SimpleWashService = new WashService()
        {
            Id = 1,
            Enabled = true,
            Name = "Ополаскивание кузова",
            WashServiceSettingsList = new List<WashServiceSettings> {
                new WashServiceSettings()
                {
                    Id = 0,
                    Price = 100,
                    DurationMinutes = 10,
                    PriceGroup = PriceGroup0,
                },
                new WashServiceSettings()
                {
                    Id = 0,
                    Price = 200,
                    DurationMinutes = 20,
                    PriceGroup = PriceGroup1,
                },
                new WashServiceSettings()
                {
                    Id = 0,
                    Price = 300,
                    DurationMinutes = 30,
                    PriceGroup = PriceGroup2,
                },
                new WashServiceSettings()
                {
                    Id = 0,
                    Price = 400,
                    DurationMinutes = 40,
                    PriceGroup = PriceGroup3,
                }
            },
            Description = "wash service description",
        };

        private List<WashService> CreateListWithOneSimpleWashService()
        {
            return new List<WashService>() { SimpleWashService };
        }

        private IMapper CreateWashServicesMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new WashServicesAutoMapperProfile());
            });
            return new Mapper(config);
        }

        [Test]
        public async Task Handle_ReturnServiceWithSedanPrice()
        {
            //Arrange
            var repositoryMoq = new Mock<IRepository<WashService>>();
            repositoryMoq
                .Setup(repository => repository.GetAllAsync(It.IsAny<Expression<Func<WashService, bool>>>()))
                .Returns((Expression<Func<WashService, bool>> predicate) => Task.FromResult(CreateListWithOneSimpleWashService()));

            //Action
            GetWashServicesByCarBodyQueryHandler queryHandler = new GetWashServicesByCarBodyQueryHandler(repositoryMoq.Object);
            var canceletionTokenSource = new CancellationTokenSource();
            var carWashId = 1;
            var carBodyId = 0; // Седан
            List<WashServiceOnePriceDto> washServicesDto = await queryHandler.Handle(new GetWashServicesByCarBodyQuery(carWashId, carBodyId), canceletionTokenSource.Token);

            // Ожидаем получить цену и длительность для Седана, согласно PriceGroup0
            var expectedPrice = 100;
            var expectedDuration = 10;

            //Assert
            Assert.IsNotNull(washServicesDto);
            Assert.AreEqual(washServicesDto.Count, 1);

            var washService = washServicesDto.FirstOrDefault();
            Assert.IsNotNull(washService);
            Assert.AreEqual(washService.Id, SimpleWashService.Id);
            Assert.AreEqual(washService.Name, SimpleWashService.Name);
            Assert.AreEqual(washService.Enabled, SimpleWashService.Enabled);
            Assert.AreEqual(washService.Description, SimpleWashService.Description);
            Assert.IsNull(washService.Composition);
            Assert.AreEqual(washService.Price, expectedPrice);
            Assert.AreEqual(washService.Duration, expectedDuration);
        }

        [Test]
        public async Task Handle_ReturnServiceWithBusPrice()
        {
            //Arrange
            var repositoryMoq = new Mock<IRepository<WashService>>();
            repositoryMoq
                .Setup(repository => repository.GetAllAsync(It.IsAny<Expression<Func<WashService, bool>>>()))
                .Returns((Expression<Func<WashService, bool>> predicate) => Task.FromResult(CreateListWithOneSimpleWashService()));

            //Action
            GetWashServicesByCarBodyQueryHandler queryHandler = new GetWashServicesByCarBodyQueryHandler(repositoryMoq.Object);
            var canceletionTokenSource = new CancellationTokenSource();
            var carWashId = 1;
            var carBodyId = 9; // Автобус
            List<WashServiceOnePriceDto> washServicesDto = await queryHandler.Handle(new GetWashServicesByCarBodyQuery(carWashId, carBodyId), canceletionTokenSource.Token);

            // Ожидаем получить цену и длительность для Седана, согласно PriceGroup0
            var expectedPrice = 400;
            var expectedDuration = 40;

            //Assert
            Assert.IsNotNull(washServicesDto);
            Assert.AreEqual(washServicesDto.Count, 1);

            var washService = washServicesDto.FirstOrDefault();
            Assert.IsNotNull(washService);
            Assert.AreEqual(washService.Id, SimpleWashService.Id);
            Assert.AreEqual(washService.Name, SimpleWashService.Name);
            Assert.AreEqual(washService.Enabled, SimpleWashService.Enabled);
            Assert.AreEqual(washService.Description, SimpleWashService.Description);
            Assert.IsNull(washService.Composition);
            Assert.AreEqual(washService.Price, expectedPrice);
            Assert.AreEqual(washService.Duration, expectedDuration);
        }

        [Test]
        public async Task Handle_ReturnEmptyListForNotExistedCarBody()
        {
            //Arrange
            var repositoryMoq = new Mock<IRepository<WashService>>();
            repositoryMoq
                .Setup(repository => repository.GetAllAsync(It.IsAny<Expression<Func<WashService, bool>>>()))
                .Returns((Expression<Func<WashService, bool>> predicate) => Task.FromResult(CreateListWithOneSimpleWashService()));

            //Action
            GetWashServicesByCarBodyQueryHandler queryHandler = new GetWashServicesByCarBodyQueryHandler(repositoryMoq.Object);
            var canceletionTokenSource = new CancellationTokenSource();
            var carWashId = 1;
            var carBodyId = 999; // Не существует такого ID кузова
            List<WashServiceOnePriceDto> washServicesDto = await queryHandler.Handle(new GetWashServicesByCarBodyQuery(carWashId, carBodyId), canceletionTokenSource.Token);

            //Assert
            Assert.IsNotNull(washServicesDto);
            Assert.AreEqual(washServicesDto.Count, 0);
        }
    }
}
