using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SmsGateway
{
    [JsonConverter(typeof(StringEnumConverter))]
    internal enum SmsStatus
    {
        [EnumMember(Value = "OK")]
        Ok,
        [EnumMember(Value = "ERROR")]
        Error
    }
}
