using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Param
    {
        [DataMember(Name = "name")]
        public dynamic Name { get; set; }

        [DataMember(Name = "value")]
        public dynamic Value { get; set; }
    }
}