using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class LayoutContent
    {
        [DataMember(Name = "related_style")]
        public string RelatedStyle { get; set; }

        [DataMember(Name = "related")]
        public Tag[] Related { get; set; }

        [DataMember(Name = "medias")]
        public SectionMedia[] Medias { get; set; }

        [DataMember(Name = "feed_type")]
        public string FeedType { get; set; }

        [DataMember(Name = "explore_item_info")]
        public ExploreItemInfo ExploreItemInfo { get; set; }

        [DataMember(Name = "tabs_info")]
        public TabsInfo TabsInfo { get; set; }
    }
}