using System.Collections.Generic;
using System.Runtime.Serialization;

using Apex.Instagram.API.Response.Serializer;

using Utf8Json;

namespace Apex.Instagram.API.Tests.Maps
{
    public class DurableMap
    {
        [DataMember(Name = "dict")]
        public Dictionary<string, string> Dict { get; set; }

        [DataMember(Name = "text")]
        [JsonFormatter(typeof(DurableStringFormatter))]
        public string Text { get; set; }

        [DataMember(Name = "number")]
        public ulong? Number { get; set; }
    }
}