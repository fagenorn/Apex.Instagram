using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class DirectThread
    {
        [DataMember(Name = "thread_id")]
        public string ThreadId { get; set; }

        [DataMember(Name = "thread_v2_id")]
        public string ThreadV2Id { get; set; }

        [DataMember(Name = "users")]
        public User[] Users { get; set; }

        [DataMember(Name = "left_users")]
        public User[] LeftUsers { get; set; }

        [DataMember(Name = "items")]
        public DirectThreadItem[] Items { get; set; }

        [DataMember(Name = "last_activity_at")]
        public string LastActivityAt { get; set; }

        [DataMember(Name = "muted")]
        public bool? Muted { get; set; }

        [DataMember(Name = "is_pin")]
        public bool? IsPin { get; set; }

        [DataMember(Name = "named")]
        public bool? Named { get; set; }

        [DataMember(Name = "canonical")]
        public bool? Canonical { get; set; }

        [DataMember(Name = "pending")]
        public bool? Pending { get; set; }

        [DataMember(Name = "valued_request")]
        public bool? ValuedRequest { get; set; }

        [DataMember(Name = "thread_type")]
        public string ThreadType { get; set; }

        [DataMember(Name = "viewer_id")]
        public string ViewerId { get; set; }

        [DataMember(Name = "thread_title")]
        public string ThreadTitle { get; set; }

        [DataMember(Name = "pending_score")]
        public string PendingScore { get; set; }

        [DataMember(Name = "vc_muted")]
        public bool? VcMuted { get; set; }

        [DataMember(Name = "is_group")]
        public bool? IsGroup { get; set; }

        [DataMember(Name = "reshare_send_count")]
        public int? ReshareSendCount { get; set; }

        [DataMember(Name = "reshare_receive_count")]
        public int? ReshareReceiveCount { get; set; }

        [DataMember(Name = "expiring_media_send_count")]
        public int? ExpiringMediaSendCount { get; set; }

        [DataMember(Name = "expiring_media_receive_count")]
        public int? ExpiringMediaReceiveCount { get; set; }

        [DataMember(Name = "inviter")]
        public User Inviter { get; set; }

        [DataMember(Name = "has_older")]
        public bool? HasOlder { get; set; }

        [DataMember(Name = "has_newer")]
        public bool? HasNewer { get; set; }

        [DataMember(Name = "last_seen_at")]
        public dynamic LastSeenAt { get; set; }

        [DataMember(Name = "newest_cursor")]
        public string NewestCursor { get; set; }

        [DataMember(Name = "oldest_cursor")]
        public string OldestCursor { get; set; }

        [DataMember(Name = "is_spam")]
        public bool? IsSpam { get; set; }

        [DataMember(Name = "last_permanent_item")]
        public PermanentItem LastPermanentItem { get; set; }

        [DataMember(Name = "unseen_count")]
        public dynamic UnseenCount { get; set; }

        [DataMember(Name = "action_badge")]
        public ActionBadge ActionBadge { get; set; }

        [DataMember(Name = "last_activity_at_secs")]
        public dynamic LastActivityAtSecs { get; set; }
    }
}