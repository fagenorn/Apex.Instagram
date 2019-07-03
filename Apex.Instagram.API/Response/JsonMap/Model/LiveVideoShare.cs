using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class LiveVideoShare
    {
        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "broadcast")]
        public Broadcast Broadcast { get; set; }

        [DataMember(Name = "video_offset")]
        public int? VideoOffset { get; set; }
    }
}