using AutoMapper;
using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Handlers.CarWash.Dto;
using UseCases.Handlers.CarWash.Mappings;


namespace UseCases.Tests.Handlers.CarWashHandlers.Mappings
{
    [TestFixture]
    public class CarWashAutoMapperProfileTests
    {

        private readonly WashService SimpleWashService = new WashService()
        {
            Id = 1,
            Enabled = true,
            Name = "Cleanity",
            Description = "wash service description",
        };

        private readonly ComplexWashService ComplexWashService = new ComplexWashService()
        {
            Id = 10,
            Enabled = true,
            Name = "ComplexCleanity",
            Description = "complex wash service description",
            WashServices = new List<WashService>()
            {
                new WashService()
                {
                    Id = 1,
                    Enabled = true,
                    Name = "Cleanity",
                    Description = "wash service description",
                },
                new WashService()
                {
                    Id = 2,
                    Enabled = true,
                    Name = "Cleanity",
                    Description = "wash service description",
                },
                new WashService()
                {
                    Id = 3,
                    Enabled = true,
                    Name = "Cleanity",
                    Description = "wash service description",
                }
            }
        };

        private List<CarWash> CreateCarWashesList()
        {
            return new List<CarWash>()
            {
                new CarWash()
                {
                    Phone = "123456",
                    Name = "car wash name",
                    Longitude = "111111",
                    Latitude = "222222",
                    Location = "City Name, Address",
                    StartWorkTime = "start",
                    EndWorkTime = "end",
                    WorkSchedule = new WorkSchedule()
                    {
                        MondayBegin = "123",
                        MondayEnd = "321",
                        TuesdayBegin = "5556",
                        TuesdayEnd = "6665",
                        WednesdayBegin = "2323",
                        WednesdayEnd = "3232",
                        ThursdayBegin = "7878",
                        ThursdayEnd = "8787",
                        FridayBegin = "3345345",
                        FridayEnd = "2323423",
                        SaturdayBegin = "6768",
                        SaturdayEnd = "96867",
                        SundayBegin = "1231",
                        SundayEnd = "4134314",
                    },
                    BoxesQuantity = 1,
                    Services = new List<WashService>()
                    {
                        SimpleWashService,
                        SimpleWashService,
                        ComplexWashService
                    }
                }
            };
        }

        private IMapper CreateWashServicesMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new CarWashAutoMapperProfile());
            });
            return new Mapper(config);
        }

        [Test]
        public void Map_MapCarWashListToCarWashListResultDto_ListEqualToDto()
        {
            //Arrange
            var mapper = CreateWashServicesMapper();

            //Act
            var result = mapper.Map<CarWashesListResultDto>(CreateCarWashesList());

            //Arrange
            Assert.AreEqual(result.carWashDtos.Count, CreateCarWashesList().Count);
        }
    }
}
