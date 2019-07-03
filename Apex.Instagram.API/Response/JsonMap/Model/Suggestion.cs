using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Suggestion
    {
        [DataMember(Name = "media_infos")]
        public dynamic MediaInfos { get; set; }

        [DataMember(Name = "social_context")]
        public string SocialContext { get; set; }

        [DataMember(Name = "algorithm")]
        public string Algorithm { get; set; }

        [DataMember(Name = "thumbnail_urls")]
        public Uri[] ThumbnailUrls { get; set; }

        [DataMember(Name = "value")]
        public double? Value { get; set; }

        [DataMember(Name = "caption")]
        public dynamic Caption { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "large_urls")]
        public Uri[] LargeUrls { get; set; }

        [DataMember(Name = "media_ids")]
        public dynamic MediaIds { get; set; }

        [DataMember(Name = "icon")]
        public dynamic Icon { get; set; }

        [DataMember(Name = "is_new_suggestion")]
        public bool? IsNewSuggestion { get; set; }

        [DataMember(Name = "uuid")]
        public string Uuid { get; set; }
    }
}