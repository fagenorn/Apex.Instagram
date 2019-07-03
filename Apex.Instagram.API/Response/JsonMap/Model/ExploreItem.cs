using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class ExploreItem
    {
        [DataMember(Name = "media")]
        public Item Media { get; set; }

        [DataMember(Name = "stories")]
        public Stories Stories { get; set; }

        [DataMember(Name = "channel")]
        public Channel Channel { get; set; }

        [DataMember(Name = "explore_item_info")]
        public ExploreItemInfo ExploreItemInfo { get; set; }
    }
}