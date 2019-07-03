using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Badging
    {
        [DataMember(Name = "ids")]
        public dynamic Ids { get; set; }

        [DataMember(Name = "items")]
        public dynamic Items { get; set; }
    }
}