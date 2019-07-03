using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class ReelShare
    {
        [DataMember(Name = "tray")]
        public Item[] Tray { get; set; }

        [DataMember(Name = "story_ranking_token")]
        public string StoryRankingToken { get; set; }

        [DataMember(Name = "broadcasts")]
        public dynamic Broadcasts { get; set; }

        [DataMember(Name = "sticker_version")]
        public int? StickerVersion { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "is_reel_persisted")]
        public bool IsReelPersisted { get; set; }

        [DataMember(Name = "reel_owner_id")]
        public string ReelOwnerId { get; set; }

        [DataMember(Name = "reel_type")]
        public string ReelType { get; set; }

        [DataMember(Name = "media")]
        public Item Media { get; set; }

        [DataMember(Name = "mentioned_user_id")]
        public string MentionedUserId { get; set; }
    }
}