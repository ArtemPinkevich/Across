namespace UseCases.Handlers.Records.Commands
{
    using MediatR;
    using UseCases.Handlers.Records.Dto;

    public class AddOrUpdateRecordCommand : IRequest<RecordDto>
    {
        public int CarWashId { get; }

        public RecordDto RecordDto { get; }

        public AddOrUpdateRecordCommand(int carWashId, RecordDto recordDto)
        {
            CarWashId = carWashId;
            RecordDto = recordDto;
        }
    }
}
