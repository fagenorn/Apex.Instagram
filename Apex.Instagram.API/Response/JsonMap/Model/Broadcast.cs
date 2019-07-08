using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Broadcast
    {
        [DataMember(Name = "broadcast_owner")]
        public User BroadcastOwner { get; set; }

        [DataMember(Name = "cobroadcasters")]
        public dynamic Cobroadcasters { get; set; }

        [DataMember(Name = "broadcast_status")]
        public string BroadcastStatus { get; set; }

        [DataMember(Name = "is_gaming_content")]
        public bool? IsGamingContent { get; set; }

        [DataMember(Name = "is_player_live_trace_enabled")]
        public bool? IsPlayerLiveTraceEnabled { get; set; }

        [DataMember(Name = "dash_live_predictive_playback_url")]
        public string DashLivePredictivePlaybackUrl { get; set; }

        [DataMember(Name = "cover_frame_url")]
        public Uri CoverFrameUrl { get; set; }

        [DataMember(Name = "published_time")]
        public ulong? PublishedTime { get; set; }

        [DataMember(Name = "hide_from_feed_unit")]
        public bool? HideFromFeedUnit { get; set; }

        [DataMember(Name = "broadcast_message")]
        public string BroadcastMessage { get; set; }

        [DataMember(Name = "muted")]
        public dynamic Muted { get; set; }

        [DataMember(Name = "media_id")]
        public string MediaId { get; set; }

        [DataMember(Name = "id")]
        public ulong? Id { get; set; }

        [DataMember(Name = "rtmp_playback_url")]
        public Uri RtmpPlaybackUrl { get; set; }

        [DataMember(Name = "dash_abr_playback_url")]
        public Uri DashAbrPlaybackUrl { get; set; }

        [DataMember(Name = "dash_playback_url")]
        public Uri DashPlaybackUrl { get; set; }

        [DataMember(Name = "ranked_position")]
        public dynamic RankedPosition { get; set; }

        [DataMember(Name = "organic_tracking_token")]
        public string OrganicTrackingToken { get; set; }

        [DataMember(Name = "seen_ranked_position")]
        public dynamic SeenRankedPosition { get; set; }

        [DataMember(Name = "viewer_count")]
        public double? ViewerCount { get; set; }

        [DataMember(Name = "dash_manifest")]
        public string DashManifest { get; set; }

        [DataMember(Name = "expire_at")]
        public ulong? ExpireAt { get; set; }

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