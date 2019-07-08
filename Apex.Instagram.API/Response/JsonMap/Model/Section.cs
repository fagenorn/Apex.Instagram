using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Section
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "items")]
        public Item[] Items { get; set; }

        [DataMember(Name = "layout_type")]
        public string LayoutType { get; set; }

        [DataMember(Name = "layout_content")]
        public LayoutContent LayoutContent { get; set; }

        [DataMember(Name = "feed_type")]
        public string FeedType { get; set; }

        [DataMember(Name = "explore_item_info")]
        public ExploreItemInfo ExploreItemInfo { get; set; }
    }
}