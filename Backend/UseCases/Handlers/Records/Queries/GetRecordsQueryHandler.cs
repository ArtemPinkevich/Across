namespace UseCases.Handlers.Records.Queries
{
    using AutoMapper;
    using DataAccess.Interfaces;
    using Entities;
    using Entities.Enums;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.Records.Dto;

    public class GetRecordsQueryHandler : IRequestHandler<GetRecordsQuery, List<RecordDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Record> _recordsRepository;

        public GetRecordsQueryHandler(IMapper mapper, IRepository<Record> recordsRepository)
        {
            _recordsRepository = recordsRepository;
            _mapper = mapper;
        }

        public async Task<List<RecordDto>> Handle(GetRecordsQuery request, CancellationToken cancellationToken)
        {
            if(!DateOnly.TryParseExact(request.SelectedDate, "yyyy.MM.dd", out DateOnly selectedDate))
            {
                return new List<RecordDto>();
            }

            List<Record> allRecords = await _recordsRepository.GetAllAsync(o => o.CarWashId == request.CarWashId
                                                                                    && o.Status == RecordStatus.Ok);
            List<Record> filtredRecords = allRecords.Where(o => DateOnly.Parse(o.Date) == selectedDate).ToList();

            List<RecordDto> recordsDto = filtredRecords.Select(o => _mapper.Map<RecordDto>(o)).ToList();

            return recordsDto;
        }
    }
}
