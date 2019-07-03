using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class PostLive
    {
        [DataMember(Name = "post_live_items")]
        public PostLiveItem[] PostLiveItems { get; set; }
    }
}