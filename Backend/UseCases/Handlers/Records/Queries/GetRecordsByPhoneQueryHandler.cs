namespace UseCases.Handlers.Records.Queries
{
    using AutoMapper;
    using DataAccess.Interfaces;
    using Entities;
    using MediatR;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.Records.Dto;
    using Entities.Enums;

    public class GetRecordsByPhoneQueryHandler : IRequestHandler<GetRecordsByPhoneQuery, List<RecordDto>>
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Record> _recordsRepository;

        public GetRecordsByPhoneQueryHandler(IMapper mapper, IRepository<Record> recordsRepository)
        {
            _recordsRepository = recordsRepository;
            _mapper = mapper;
        }

        public async Task<List<RecordDto>> Handle(GetRecordsByPhoneQuery request, CancellationToken cancellationToken)
        {
            List<Record> records = await _recordsRepository.GetAllAsync(o => o.PhoneNumber == request.PhoneNumber && o.Status == RecordStatus.Ok);
            List<RecordDto> recordsDto = records.Select(o => _mapper.Map<RecordDto>(o)).ToList();

            return recordsDto;
        }
    }
}
