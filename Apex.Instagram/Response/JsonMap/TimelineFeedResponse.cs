using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class TimelineFeedResponse : Response
    {
        [DataMember(Name = "num_results")]
        public int? NumResults { get; set; }

        [DataMember(Name = "client_gap_enforcer_matrix")]
        public dynamic ClientGapEnforcerMatrix { get; set; }

        [DataMember(Name = "is_direct_v2_enabled")]
        public bool? IsDirectV2Enabled { get; set; }

        [DataMember(Name = "auto_load_more_enabled")]
        public bool? AutoLoadMoreEnabled { get; set; }

        [DataMember(Name = "more_available")]
        public bool? MoreAvailable { get; set; }

        [DataMember(Name = "next_max_id")]
        public string NextMaxId { get; set; }

        [DataMember(Name = "pagination_info")]
        public dynamic PaginationInfo { get; set; }

        [DataMember(Name = "feed_items")]
        public FeedItem[] FeedItems { get; set; }

        [DataMember(Name = "megaphone")]
        public FeedAysf Megaphone { get; set; }

        [DataMember(Name = "client_feed_changelist_applied")]
        public bool? ClientFeedChangelistApplied { get; set; }

        [DataMember(Name = "view_state_version")]
        public string ViewStateVersion { get; set; }

        [DataMember(Name = "feed_pill_text")]
        public string FeedPillText { get; set; }

        [DataMember(Name = "client_session_id")]
        public string ClientSessionId { get; set; }
    }
}