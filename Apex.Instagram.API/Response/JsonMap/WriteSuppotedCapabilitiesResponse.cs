using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class WriteSuppotedCapabilitiesResponse : Response
    {
        [DataMember(Name = "supported_capabilities")]
        public SupportedCapabilities[] SupportedCapabilities { get; set; }
    }
}