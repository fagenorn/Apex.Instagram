using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class PostLiveItem
    {
        [DataMember(Name = "pk")]
        public string Pk { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "broadcasts")]
        public Broadcast[] Broadcasts { get; set; }

        [DataMember(Name = "peak_viewer_count")]
        public int? PeakViewerCount { get; set; }

        [DataMember(Name = "last_seen_broadcast_ts")]
        public dynamic LastSeenBroadcastTs { get; set; }

        [DataMember(Name = "can_reply")]
        public dynamic CanReply { get; set; }

        [DataMember(Name = "ranked_position")]
        public dynamic RankedPosition { get; set; }

        [DataMember(Name = "seen_ranked_position")]
        public dynamic SeenRankedPosition { get; set; }

        [DataMember(Name = "muted")]
        public dynamic Muted { get; set; }

        [DataMember(Name = "can_reshare")]
        public dynamic CanReshare { get; set; }
    }
}