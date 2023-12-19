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
using UseCases.Handlers.WashServices.Mappings;
using UseCases.Handlers.WashServices.Queries;

namespace UseCases.Tests.Handlers.WashServices.Queries
{
    [TestFixture]
    public class GetWashServicesQueryHandlerTests
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
            Id = 1,
            Enabled = true,
            Name = "Cleanity",
            Description = "wash service description",
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
        public async Task Handle_RepositoryContainsOneSimpleWashService_QueryHandlerReturnOneSimpleServiceDto()
        {
            //Arrange
            var mapper = CreateWashServicesMapper();
            var repositoryMoq = new Mock<IRepository<WashService>>();
            repositoryMoq
                .Setup(repository => repository.GetAllAsync(It.IsAny<Expression<Func<WashService, bool>>>()))
                .Returns((Expression<Func<WashService, bool>> predicate) => Task.FromResult(CreateListWithOneSimpleWashService()));

            //Action
            GetWashServicesQueryHandler queryHandler = new GetWashServicesQueryHandler(mapper, repositoryMoq.Object);
            var canceletionTokenSource = new CancellationTokenSource();
            var washServicesDto = await queryHandler.Handle(new GetWashServicesQuery(1, true), canceletionTokenSource.Token);


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
        public async Task Handle_RepositoryContainsOneComplexWashService_QueryHandlerReturnOneComplexServiceDto()
        {
            //Arrange
            var mapper = CreateWashServicesMapper();
            var repositoryMoq = new Mock<IRepository<WashService>>();
            repositoryMoq
                .Setup(repository => repository.GetAllAsync(It.IsAny<Expression<Func<WashService, bool>>>()))
                .Returns((Expression<Func<WashService, bool>> predicate) => Task.FromResult(CreateListWithOneComplexWashService()));

            //Action
            GetWashServicesQueryHandler queryHandler = new GetWashServicesQueryHandler(mapper, repositoryMoq.Object);
            var canceletionTokenSource = new CancellationTokenSource();
            var washServicesDto = await queryHandler.Handle(new GetWashServicesQuery(1, true), canceletionTokenSource.Token);


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
            Assert.IsNotNull(washService.Composition);
            Assert.AreEqual(washService.Composition.Count, 3);
        }
    }
}
