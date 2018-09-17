using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class PermanentItem
    {
        [DataMember(Name = "item_id")]
        public string ItemId { get; set; }

        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }

        [DataMember(Name = "item_type")]
        public string ItemType { get; set; }

        [DataMember(Name = "profile")]
        public User Profile { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "like")]
        public dynamic Like { get; set; }

        [DataMember(Name = "media")]
        public MediaData Media { get; set; }

        [DataMember(Name = "link")]
        public Link Link { get; set; }

        [DataMember(Name = "media_share")]
        public Item MediaShare { get; set; }

        [DataMember(Name = "reel_share")]
        public ReelShare ReelShare { get; set; }

        [DataMember(Name = "client_context")]
        public string ClientContext { get; set; }

        [DataMember(Name = "live_video_share")]
        public LiveVideoShare LiveVideoShare { get; set; }
    }
}