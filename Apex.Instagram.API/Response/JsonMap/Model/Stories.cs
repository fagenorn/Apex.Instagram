using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Stories
    {
        [DataMember(Name = "is_portrait")]
        public dynamic IsPortrait { get; set; }

        [DataMember(Name = "tray")]
        public StoryTray[] Tray { get; set; }

        [DataMember(Name = "id")]
        public ulong? Id { get; set; }

        [DataMember(Name = "top_live")]
        public TopLive TopLive { get; set; }
    }
}