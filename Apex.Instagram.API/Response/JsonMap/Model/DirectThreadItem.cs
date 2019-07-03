using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class DirectThreadItem
    {
        [DataMember(Name = "item_id")]
        public string ItemId { get; set; }

        [DataMember(Name = "item_type")]
        public dynamic ItemType { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "media_share")]
        public Item MediaShare { get; set; }

        [DataMember(Name = "preview_medias")]
        public Item[] PreviewMedias { get; set; }

        [DataMember(Name = "media")]
        public DirectThreadItemMedia Media { get; set; }

        [DataMember(Name = "user_id")]
        public ulong? UserId { get; set; }

        [DataMember(Name = "timestamp")]
        public dynamic Timestamp { get; set; }

        [DataMember(Name = "client_context")]
        public string ClientContext { get; set; }

        [DataMember(Name = "hide_in_thread")]
        public dynamic HideInThread { get; set; }

        [DataMember(Name = "action_log")]
        public ActionLog ActionLog { get; set; }

        [DataMember(Name = "link")]
        public DirectLink Link { get; set; }

        [DataMember(Name = "reactions")]
        public DirectReactions Reactions { get; set; }

        [DataMember(Name = "raven_media")]
        public Item RavenMedia { get; set; }

        [DataMember(Name = "seen_user_ids")]
        public string[] SeenUserIds { get; set; }

        [DataMember(Name = "expiring_media_action_summary")]
        public DirectExpiringSummary ExpiringMediaActionSummary { get; set; }

        [DataMember(Name = "reel_share")]
        public ReelShare ReelShare { get; set; }

        [DataMember(Name = "placeholder")]
        public Placeholder Placeholder { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "like")]
        public dynamic Like { get; set; }

        [DataMember(Name = "live_video_share")]
        public LiveVideoShare LiveVideoShare { get; set; }

        [DataMember(Name = "live_viewer_invite")]
        public LiveViewerInvite LiveViewerInvite { get; set; }

        [DataMember(Name = "profile")]
        public User Profile { get; set; }

        [DataMember(Name = "story_share")]
        public StoryShare StoryShare { get; set; }

        [DataMember(Name = "direct_media_share")]
        public MediaShare DirectMediaShare { get; set; }

        [DataMember(Name = "video_call_event")]
        public VideoCallEvent VideoCallEvent { get; set; }

        [DataMember(Name = "product_share")]
        public ProductShare ProductShare { get; set; }

        [DataMember(Name = "animated_media")]
        public AnimatedMedia AnimatedMedia { get; set; }

        [DataMember(Name = "felix_share")]
        public FelixShare FelixShare { get; set; }
    }
}