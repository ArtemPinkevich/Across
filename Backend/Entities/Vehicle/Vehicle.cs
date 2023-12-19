namespace Entities
{
    public class Vehicle : EntityBase
    {
        /// <summary>
        /// Идентификатор сгенерированный на стороне мобилки
        /// </summary>
        public string CreatedId { set; get; }

        public string RegNumber { set; get; }

        public string Mark { set; get; }

        public string Model { set; get; }

        public CarBody CarBody { set; get; }

        //владелец транспортного средства
        public string UserId { set; get; }
        public User User { set; get; }
    }
}
