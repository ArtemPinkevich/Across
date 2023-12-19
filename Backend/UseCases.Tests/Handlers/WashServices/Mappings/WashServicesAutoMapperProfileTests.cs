using AutoMapper;
using Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.Handlers.Records.Dto;
using UseCases.Handlers.WashServices.Dto;
using UseCases.Handlers.WashServices.Mappings;

namespace UseCases.Tests.Handlers.WashServices.Mappings
{
    [TestFixture]
    public class WashServicesAutoMapperProfileTests
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

        private List<WashService> CreateListWithOneSimpleWashService()
        {
            return new List<WashService>() { SimpleWashService };
        }

        private List<WashService> CreateListWithOneComplexWashService()
        {
            return new List<WashService>() { ComplexWashService };
        }

        private IMapper CreateWashServicesMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new WashServicesAutoMapperProfile());
            });
            return new Mapper(config);
        }

        [Test]
        public void Map_MapSimpleWashServiceToWashServiceDto_WashServiceEqualWashServiceDto()
        {
            //Arrange
            var mapper = CreateWashServicesMapper();

            //Act
            var washServiceDto = mapper.Map<WashServiceDto>(SimpleWashService);

            //Arrange
            Assert.IsNotNull(washServiceDto);
            Assert.AreEqual(washServiceDto.Id, SimpleWashService.Id);
            Assert.AreEqual(washServiceDto.Name, SimpleWashService.Name);
            Assert.AreEqual(washServiceDto.Enabled, SimpleWashService.Enabled);
            Assert.AreEqual(washServiceDto.Description, SimpleWashService.Description);
            Assert.IsNull(washServiceDto.Composition);
        }

        [Test]
        public void Map_MapComplexWashServiceToWashServiceDto_CompelxWashServiceEqualWashServiceDto()
        {
            //Arrange
            var mapper = CreateWashServicesMapper();

            //Act
            var washServiceDto = mapper.Map<WashServiceDto>(ComplexWashService);

            //Arrange
            Assert.IsNotNull(washServiceDto);
            Assert.AreEqual(washServiceDto.Id, ComplexWashService.Id);
            Assert.AreEqual(washServiceDto.Name, ComplexWashService.Name);
            Assert.AreEqual(washServiceDto.Enabled, ComplexWashService.Enabled);
            Assert.AreEqual(washServiceDto.Description, ComplexWashService.Description);

            Assert.IsNotNull(washServiceDto.Composition);
            Assert.AreEqual(washServiceDto.Composition.Count, 3);
            Assert.IsTrue(washServiceDto.Composition.Contains(1));
            Assert.IsTrue(washServiceDto.Composition.Contains(2));
            Assert.IsTrue(washServiceDto.Composition.Contains(3));
        }

        [Test]
        public void Map_MapListOfSimpleWashServicesToGetWashServicesDto_ListIfWashServiesEqualToGetWashServicesDto()
        {
            //Arrange
            var mapper = CreateWashServicesMapper();

            //Act
            var washServicesDto = mapper.Map<GetWashServicesDto>(CreateListWithOneSimpleWashService());

            //Assert
            Assert.IsNotNull(washServicesDto);
            Assert.IsNotNull(washServicesDto.AllWashServices);
            Assert.AreEqual(washServicesDto.AllWashServices.Count, 1);

            var washService = washServicesDto.AllWashServices.FirstOrDefault();
            Assert.IsNotNull(washService);
            Assert.AreEqual(washService.Id, SimpleWashService.Id);
            Assert.AreEqual(washService.Name, SimpleWashService.Name);
            Assert.AreEqual(washService.Enabled, SimpleWashService.Enabled);
            Assert.AreEqual(washService.Description, SimpleWashService.Description);
            Assert.IsNull(washService.Composition);
        }

        [Test]
        public void Map_MapListOfComplexWashServicesToGetWashServicesDto_ListIfWashServiesEqualToGetWashServicesDto()
        {
            //Arrange
            var mapper = CreateWashServicesMapper();

            //Act
            var washServicesDto = mapper.Map<GetWashServicesDto>(CreateListWithOneComplexWashService());

            //Assert
            Assert.IsNotNull(washServicesDto);
            Assert.IsNotNull(washServicesDto.AllWashServices);
            Assert.AreEqual(washServicesDto.AllWashServices.Count, 1);

            var washService = washServicesDto.AllWashServices.FirstOrDefault();
            Assert.IsNotNull(washService);
            Assert.AreEqual(washService.Id, ComplexWashService.Id);
            Assert.AreEqual(washService.Name, ComplexWashService.Name);
            Assert.AreEqual(washService.Enabled, ComplexWashService.Enabled);
            Assert.AreEqual(washService.Description, ComplexWashService.Description);
            Assert.IsNotNull(washService.Composition);
            Assert.AreEqual(washService.Composition.Count, 3);
        }
    }
}
