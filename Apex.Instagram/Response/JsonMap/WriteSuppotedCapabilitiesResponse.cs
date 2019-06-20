using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class WriteSuppotedCapabilitiesResponse : Response
    {
        [DataMember(Name = "supported_capabilities")]
        public SupportedCapabilities[] SupportedCapabilities { get; set; }
    }
}