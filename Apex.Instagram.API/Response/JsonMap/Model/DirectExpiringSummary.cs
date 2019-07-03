using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class DirectExpiringSummary
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }

        [DataMember(Name = "count")]
        public int? Count { get; set; }
    }
}