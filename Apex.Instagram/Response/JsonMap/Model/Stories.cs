using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Stories
    {
        [DataMember(Name = "is_portrait")]
        public dynamic IsPortrait { get; set; }

        [DataMember(Name = "tray")]
        public StoryTray[] Tray { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "top_live")]
        public TopLive TopLive { get; set; }
    }
}