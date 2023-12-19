using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseCases.Handlers.Registration.Dto
{
    public class CarWashRegistrationResult: RegistrationDto
    {
        public int? CarWashId { set; get; }
    }
}
