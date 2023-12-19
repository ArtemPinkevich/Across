namespace UseCases.Handlers.Records.Queries
{
    using MediatR;
    using UseCases.Handlers.Records.Dto;

    public class GetFreeTimeSlotsQuery : IRequest<FreeTimePointsResponceDto>
    {
        public int CarWashId { get; }
        public string SelectedDate { set; get; }
        public int DurationMin { set; get; }

        public GetFreeTimeSlotsQuery(int carWashId, string selectedDate, int durationMin)
        {
            CarWashId = carWashId;
            SelectedDate = selectedDate;
            DurationMin = durationMin;
        }
    }
}
