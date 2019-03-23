using System.Runtime.Serialization;

using Apex.Instagram.Response.Serializer;

using Utf8Json;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class StoryTray
    {
        [DataMember(Name = "id")]
        [JsonFormatter(typeof(DurableStringFormatter))]
        public string Id { get; set; }

        [DataMember(Name = "items")]
        public Item[] Items { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "can_reply")]
        public dynamic CanReply { get; set; }

        [DataMember(Name = "expiring_at")]
        public dynamic ExpiringAt { get; set; }

        [DataMember(Name = "seen_ranked_position")]
        public int? SeenRankedPosition { get; set; }

        /// <summary>
        ///     The "taken_at" timestamp of the last story media you have seen for
        ///     that user(the current tray's user). Defaults to `0` (not seen).
        /// </summary>
        [DataMember(Name = "seen")]
        public double? Seen { get; set; }

        /// <summary>
        ///     Unix "taken_at" timestamp of the newest item in their story reel.
        /// </summary>
        [DataMember(Name = "latest_reel_media")]
        public ulong? LatestReelMedia { get; set; }

        [DataMember(Name = "ranked_position")]
        public int? RankedPosition { get; set; }

        [DataMember(Name = "is_nux")]
        public dynamic IsNux { get; set; }

        [DataMember(Name = "show_nux_tooltip")]
        public dynamic ShowNuxTooltip { get; set; }

        [DataMember(Name = "muted")]
        public dynamic Muted { get; set; }

        [DataMember(Name = "prefetch_count")]
        public int? PrefetchCount { get; set; }

        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "source_token")]
        public dynamic SourceToken { get; set; }

        [DataMember(Name = "owner")]
        public Owner Owner { get; set; }

        [DataMember(Name = "nux_id")]
        public string NuxId { get; set; }

        [DataMember(Name = "dismiss_card")]
        public DismissCard DismissCard { get; set; }

        [DataMember(Name = "can_reshare")]
        public dynamic CanReshare { get; set; }

        [DataMember(Name = "has_besties_media")]
        public bool? HasBestiesMedia { get; set; }

        [DataMember(Name = "reel_type")]
        public string ReelType { get; set; }

        [DataMember(Name = "unique_integer_reel_id")]
        public ulong? UniqueIntegerReelId { get; set; }

        [DataMember(Name = "cover_media")]
        public CoverMedia CoverMedia { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "media_count")]
        public int? MediaCount { get; set; }
    }
}