using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap
{
    public class CapabilitiesDecisionsResponse : Response

    {
        [DataMember(Name = "decisions")]
        public dynamic Decisions { get; set; }
    }
}