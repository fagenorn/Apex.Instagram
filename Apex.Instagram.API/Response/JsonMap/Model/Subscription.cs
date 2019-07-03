using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Subscription
    {
        [DataMember(Name = "topic")]
        public dynamic Topic { get; set; }

        [DataMember(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "sequence")]
        public dynamic Sequence { get; set; }

        [DataMember(Name = "auth")]
        public dynamic Auth { get; set; }
    }
}