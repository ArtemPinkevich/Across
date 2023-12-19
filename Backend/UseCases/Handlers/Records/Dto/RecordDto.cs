namespace UseCases.Handlers.Records.Dto
{
    using System.Collections.Generic;

    public class RecordDto
    {
        public int? Id { set; get; }

        public int CarWashId { set; get; }

        public int BoxId { set; get; }

        public string ClientId { set; get; }

        public CarInfoDto CarInfo { set; get; }

        public int MainServiceId { get; set; }

        public List<int> AdditionServicesIds { set; get; }

        // Строка формата yyyy.MM.dd, например "2022.01.30"
        public string Date { set; get; }

        // Строка формата TimeOnly, например "08:00"
        public string StartTime { set; get; }

        public int Price { set; get; }

        public int DurationMin { set; get; }

        public string PhoneNumber { set; get; }
    }
}
