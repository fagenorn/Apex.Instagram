using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class AdMetadata
    {
        [DataMember(Name = "value")]
        public dynamic Value { get; set; }

        [DataMember(Name = "type")]
        public dynamic Type { get; set; }
    }
}