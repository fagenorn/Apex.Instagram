using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class VideoCallEvent
    {
        [DataMember(Name = "action")]
        public string Action { get; set; }

        [DataMember(Name = "vc_id")]
        public string VcId { get; set; }
    }
}