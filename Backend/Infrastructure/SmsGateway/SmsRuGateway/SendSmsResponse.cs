using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SmsGateway
{
    internal class SendSmsResponse
    {
        [JsonProperty("status")]
        public SmsStatus Status { set; get; }

        [JsonProperty("status_code")]
        public int StatusCode { set; get; }

        [JsonProperty("balance")]
        public float Balance { set; get; }
    }
}
