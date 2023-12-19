namespace UseCases.Handlers.Records.Queries
{
    using MediatR;
    using System.Collections.Generic;
    using UseCases.Handlers.Records.Dto;

    public class GetRecordsQuery : IRequest<List<RecordDto>>
    {
        public int CarWashId { get; }
        public string SelectedDate { set; get; }

        public GetRecordsQuery(int carWashId, string selectedDate)
        {
            CarWashId = carWashId;
            SelectedDate = selectedDate;
        }
    }
}
