using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class VideoCallEvent
    {
        [DataMember(Name = "action")]
        public string Action { get; set; }

        [DataMember(Name = "vc_id")]
        public ulong? VcId { get; set; }
    }
}