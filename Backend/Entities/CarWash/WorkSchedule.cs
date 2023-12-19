namespace Entities
{
    public class WorkSchedule: EntityBase
    {
        // Строки должны иметь формат TimeOnly, например "08:00"
        public string MondayBegin { set; get; }
        public string MondayEnd { set; get; }
        public string TuesdayBegin { set; get; }
        public string TuesdayEnd { set; get; }
        public string WednesdayBegin { set; get; }
        public string WednesdayEnd { set; get; }
        public string ThursdayBegin { set; get; }
        public string ThursdayEnd { set; get; }
        public string FridayBegin { set; get; }
        public string FridayEnd { set; get; }
        public string SaturdayBegin { set; get; }
        public string SaturdayEnd { set; get; }
        public string SundayBegin { set; get; }
        public string SundayEnd { set; get; }

        /// <summary>
        /// Возвращает в каком часовом поясе работает автомойка (смещение по UTC)
        /// </summary>
        public int UtcOffset { get; set; }

        public int CarWashId { set; get; }
        public CarWash CarWash { set; get; }
    }
}
