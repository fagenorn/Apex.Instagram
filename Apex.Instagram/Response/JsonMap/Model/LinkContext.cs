using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class LinkContext
    {
        [DataMember(Name = "link_url")]
        public string LinkUrl { get; set; }

        [DataMember(Name = "link_title")]
        public string LinkTitle { get; set; }

        [DataMember(Name = "link_summary")]
        public string LinkSummary { get; set; }

        [DataMember(Name = "link_image_url")]
        public string LinkImageUrl { get; set; }
    }
}