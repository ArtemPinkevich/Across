namespace UseCases.Handlers.CarWash.Dto
{
    using System.Collections.Generic;

    public class CarWashDto
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string Location { set; get; }

        public string Longitude { set; get; }

        public string Latitude { set; get; }

        public int BoxesQuantity { set; get; }

        public int ReservedMinutesBetweenRecords { set; get; }

        public string Phone { set; get; }

        public WorkTimeDto WorkTime { set; get; }
    }
}
