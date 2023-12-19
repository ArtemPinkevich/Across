using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Handlers.CarWash.Dto
{
    public class ComplexWashServiceDto: BaseWashServiceDto
    {
        public List<int> ComplexServices { set; get; }
    }
}
