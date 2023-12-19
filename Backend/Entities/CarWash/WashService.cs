namespace Entities
{
    using System.Collections.Generic;

    public class WashService : EntityBase
    {
        public bool Enabled { set; get; } = true;
        public string Name { set; get; }
        public string Description { set; get; }

        public int CarWashId { set; get; }
        public CarWash CarWash { set; get; }

        public List<WashServiceSettings> WashServiceSettingsList { set; get; }
        
        public List<ComplexWashService> ComplexWashServices { set; get; }

        public List<Record> Records { set; get; }
    }
}
