using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap
{
    public class FacebookOtaResponse : Response
    {
        [DataMember(Name = "bundles")]
        public dynamic Bundles { get; set; }

        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }
    }
}