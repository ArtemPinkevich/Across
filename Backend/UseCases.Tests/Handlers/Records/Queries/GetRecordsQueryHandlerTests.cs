namespace UseCases.Tests.Handlers.Records.Queries
{
    using AutoMapper;
    using DataAccess.Interfaces;
    using Entities;
    using NSubstitute;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.Records.Dto;
    using UseCases.Handlers.Records.Mapping;
    using UseCases.Handlers.Records.Queries;

    [TestFixture]
    public class GetRecordsQueryHandlerTests
    {
        private IMapper CreateRecordsMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile(new RecordAutoMapperProfile());
            });
            return new Mapper(config);
        }

        [Test]
        public async Task Handle_ReturnCorrectListAsync()
        {
            //Arrange
            int carWashId = 1;
            var selectedDate = "2021.10.17";
            var _recordRepositoryMock = Substitute.For<IRepository<Record>>();
            var expectedRightId = 777;

            var recordFake = new Record()
            {
                Id = expectedRightId,
                CarWashId = carWashId,
                Date = "2021.10.17",    // выбранный день
                StartTime = "10:00",
            };

            var recordFake2 = new Record()
            {
                Id = 2,
                CarWashId = carWashId,
                Date = "2021.10.18",    // следующий день от выбранного
                StartTime = "14:00",
            };

            var recordFake3 = new Record()
            {
                Id = 3,
                CarWashId = carWashId,
                Date = "2021.10.16",    // предыдущий день от выбранного
                StartTime = "14:10",
            };

            _recordRepositoryMock.GetAllAsync(default).ReturnsForAnyArgs(new List<Record>() { recordFake, recordFake2, recordFake3 });

            var getRecordsQueryHandler = new GetRecordsQueryHandler(CreateRecordsMapper(), _recordRepositoryMock);

            //Act
            var getRecordsQuery = new GetRecordsQuery(carWashId, selectedDate);
            List<RecordDto> resultRecordDtoList = await getRecordsQueryHandler.Handle(getRecordsQuery, new CancellationToken());

            //Arrange
            Assert.IsTrue(resultRecordDtoList.Count == 1);
            Assert.AreEqual(resultRecordDtoList[0].Id, expectedRightId);
        }
    }
}

