using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Tag
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "media_count")]
        public int? MediaCount { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "follow_status")]
        public dynamic FollowStatus { get; set; }

        [DataMember(Name = "following")]
        public dynamic Following { get; set; }

        [DataMember(Name = "allow_following")]
        public dynamic AllowFollowing { get; set; }

        [DataMember(Name = "allow_muting_story")]
        public dynamic AllowMutingStory { get; set; }

        [DataMember(Name = "profile_pic_url")]
        public dynamic ProfilePicUrl { get; set; }

        [DataMember(Name = "non_violating")]
        public dynamic NonViolating { get; set; }

        [DataMember(Name = "related_tags")]
        public dynamic RelatedTags { get; set; }

        [DataMember(Name = "subtitle")]
        public dynamic Subtitle { get; set; }

        [DataMember(Name = "social_context")]
        public dynamic SocialContext { get; set; }

        [DataMember(Name = "social_context_profile_links")]
        public dynamic SocialContextProfileLinks { get; set; }

        [DataMember(Name = "show_follow_drop_down")]
        public dynamic ShowFollowDropDown { get; set; }

        [DataMember(Name = "follow_button_text")]
        public dynamic FollowButtonText { get; set; }

        [DataMember(Name = "debug_info")]
        public dynamic DebugInfo { get; set; }

        [DataMember(Name = "search_result_subtitle")]
        public dynamic SearchResultSubtitle { get; set; }
    }
}