namespace UseCases.Handlers.Records.Commands
{
    using MediatR;
    using UseCases.Handlers.Records.Dto;

    public class DiscardRecordCommand : IRequest<DiscardRecordResultDto>
    {
        public int RecordId { get; }

        public DiscardRecordCommand(int recordId)
        {
            RecordId = recordId;
        }
    }
}
