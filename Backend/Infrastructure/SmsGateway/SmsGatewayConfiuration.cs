using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SmsGateway
{
    public class SmsGatewayConfiuration
    {
        public bool UseRealGateway { set; get; }

        public string ApiId { set; get; }
    }
}
