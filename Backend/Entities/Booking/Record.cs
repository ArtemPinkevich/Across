namespace Entities
{
    using Entities.Enums;
    using System.Collections.Generic;

    public class Record : EntityBase
    {
        public int CarWashId { set; get; }
        public CarWash CarWash { set; get; }

        public int BoxId { set; get; }

        public string UserId { set; get; }
        public User User { set; get; }

        public int VehicleId { set; get; }
        public Vehicle Vehicle { set; get; }

        public int MainWashServiceId { set; get; }

        public List<WashService> WashServices { set; get; }

        // Строка формата yyyy.MM.dd, например "2022.01.30"
        public string Date { set; get; }

        // Строка формата TimeOnly, например "08:00"
        public string StartTime { set; get; }

        public int TotalDurationMin { set; get; }

        public int TotalPrice { set; get; }

        public string PhoneNumber { set; get; }
        
        public RecordStatus Status { set; get; }
    }
}
