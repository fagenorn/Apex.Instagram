using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class SupportedCapabilities
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}