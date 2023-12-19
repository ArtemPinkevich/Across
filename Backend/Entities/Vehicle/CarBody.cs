namespace Entities
{
    using System.Collections.Generic;

    public class CarBody : EntityBase
    {
        public string CarBodyName { set; get; }

        public List<PriceGroup> PriceGroups { set; get; }
    }
}
