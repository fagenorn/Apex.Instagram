using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Broadcast
    {
        [DataMember(Name = "broadcast_owner")]
        public User BroadcastOwner { get; set; }

        [DataMember(Name = "broadcast_status")]
        public string BroadcastStatus { get; set; }

        [DataMember(Name = "cover_frame_url")]
        public string CoverFrameUrl { get; set; }

        [DataMember(Name = "published_time")]
        public string PublishedTime { get; set; }

        [DataMember(Name = "broadcast_message")]
        public string BroadcastMessage { get; set; }

        [DataMember(Name = "muted")]
        public dynamic Muted { get; set; }

        [DataMember(Name = "media_id")]
        public string MediaId { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "rtmp_playback_url")]
        public string RtmpPlaybackUrl { get; set; }

        [DataMember(Name = "dash_abr_playback_url")]
        public string DashAbrPlaybackUrl { get; set; }

        [DataMember(Name = "dash_playback_url")]
        public string DashPlaybackUrl { get; set; }

        [DataMember(Name = "ranked_position")]
        public dynamic RankedPosition { get; set; }

        [DataMember(Name = "organic_tracking_token")]
        public string OrganicTrackingToken { get; set; }

        [DataMember(Name = "seen_ranked_position")]
        public dynamic SeenRankedPosition { get; set; }

        [DataMember(Name = "viewer_count")]
        public int? ViewerCount { get; set; }

        [DataMember(Name = "dash_manifest")]
        public string DashManifest { get; set; }

        [DataMember(Name = "expire_at")]
        public string ExpireAt { get; set; }

        [DataMember(Name = "encoding_tag")]
        public string EncodingTag { get; set; }

        [DataMember(Name = "total_unique_viewer_count")]
        public int? TotalUniqueViewerCount { get; set; }

        [DataMember(Name = "internal_only")]
        public bool? InternalOnly { get; set; }

        [DataMember(Name = "number_of_qualities")]
        public int? NumberOfQualities { get; set; }
    }
}