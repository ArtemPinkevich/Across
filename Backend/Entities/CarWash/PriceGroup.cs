namespace Entities
{
    using System.Collections.Generic;

    public class PriceGroup : EntityBase
    {
        public string Name { get; set; }

        public string Description { set; get; }

        public int CarWashId { set; get; }
     
        public CarWash CarWash { set; get; }

        public List<CarBody> CarBodies { set; get; }
        
        public List<WashServiceSettings> WashServiceSettings { set; get; }
    }
}
