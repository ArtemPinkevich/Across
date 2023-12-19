namespace UseCases.Handlers.Records.Commands
{
    using DataAccess.Interfaces;
    using Entities;
    using Entities.Enums;
    using MediatR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using UseCases.Handlers.Records.Dto;

    public class DiscardRecordCommandHandler : IRequestHandler<DiscardRecordCommand, DiscardRecordResultDto>
    {
        private readonly IRepository<Record> _recordsRepository;

        public DiscardRecordCommandHandler(IRepository<Record> recordsRepository)
        {
            _recordsRepository = recordsRepository;
        }

        public async Task<DiscardRecordResultDto> Handle(DiscardRecordCommand query, CancellationToken cancellationToken)
        {
            DiscardRecordResultDto discardRecordResultDto = new DiscardRecordResultDto() { DiscardedRecordId = query.RecordId};

            var record = await _recordsRepository.GetAsync(o => o.Id == query.RecordId);
            if (record == null)
            {
                discardRecordResultDto.Successed = false;
                return discardRecordResultDto;
            }

            record.Status = RecordStatus.Cancelled;
            await _recordsRepository.UpdateAsync(record);
            await _recordsRepository.SaveAsync();
            
            discardRecordResultDto.Successed = true;
            return discardRecordResultDto;
        }
    }
}
