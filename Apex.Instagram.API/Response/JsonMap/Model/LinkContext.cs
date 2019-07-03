using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class LinkContext
    {
        [DataMember(Name = "link_url")]
        public Uri LinkUrl { get; set; }

        [DataMember(Name = "link_title")]
        public string LinkTitle { get; set; }

        [DataMember(Name = "link_summary")]
        public string LinkSummary { get; set; }

        [DataMember(Name = "link_image_url")]
        public Uri LinkImageUrl { get; set; }
    }
}