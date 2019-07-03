using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Bold
    {
        [DataMember(Name = "start")]
        public dynamic Start { get; set; }

        [DataMember(Name = "end")]
        public dynamic End { get; set; }
    }
}