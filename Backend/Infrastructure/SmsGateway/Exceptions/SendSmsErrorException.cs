using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SmsGateway.Exceptions
{
    public class SendSmsErrorException: Exception
    {
        public string Phone { get; }

        public int StatusCode { get; }

        public SendSmsErrorException( string phone, int statusCode, string message)
            : base(message)
        {
            Phone = phone;
            StatusCode = statusCode;
        }
    }
}
