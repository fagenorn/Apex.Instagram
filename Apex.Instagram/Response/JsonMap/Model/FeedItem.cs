using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class FeedItem
    {
        [DataMember(Name = "media_or_ad")]
        public Item MediaOrAd { get; set; }

        [DataMember(Name = "stories_netego")]
        public StoriesNetego StoriesNetego { get; set; }

        [DataMember(Name = "ad4ad")]
        public Ad4Ad Ad4Ad { get; set; }

        [DataMember(Name = "suggested_users")]
        public SuggestedUsers SuggestedUsers { get; set; }

        [DataMember(Name = "end_of_feed_demarcator")]
        public dynamic EndOfFeedDemarcator { get; set; }

        [DataMember(Name = "ad_link_type")]
        public int? AdLinkType { get; set; }
    }
}