using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Handlers.CarWash.Dto
{
    public class BaseWashServiceDto
    {
        public string Id { set; get; }

        public string Name { set; get; }

        public int DurationMinutes { set; get; }
        
        public int Price { set; get; }

        public string Description { set; get; }

        public List<int> ServicesIds { set; get; } 
    }
}
