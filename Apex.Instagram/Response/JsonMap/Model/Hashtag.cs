using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Hashtag
    {
        [DataMember(Name = "id")]
        public ulong? Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "media_count")]
        public int? MediaCount { get; set; }

        [DataMember(Name = "profile_pic_url")]
        public Uri ProfilePicUrl { get; set; }

        [DataMember(Name = "follow_status")]
        public int? FollowStatus { get; set; }

        [DataMember(Name = "following")]
        public int? Following { get; set; }

        [DataMember(Name = "allow_following")]
        public int? AllowFollowing { get; set; }

        [DataMember(Name = "allow_muting_story")]
        public bool? AllowMutingStory { get; set; }

        [DataMember(Name = "related_tags")]
        public dynamic RelatedTags { get; set; }

        [DataMember(Name = "debug_info")]
        public dynamic DebugInfo { get; set; }
    }
}