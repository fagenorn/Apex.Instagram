using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Args
    {
        [DataMember(Name = "media_destination")]
        public string MediaDestination { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "links")]
        public Link[] Links { get; set; }

        [DataMember(Name = "profile_id")]
        public ulong? ProfileId { get; set; }

        [DataMember(Name = "profile_image")]
        public string ProfileImage { get; set; }

        [DataMember(Name = "media")]
        public Media[] Media { get; set; }

        [DataMember(Name = "timestamp")]
        public double? Timestamp { get; set; }

        [DataMember(Name = "tuuid")]
        public string Tuuid { get; set; }

        [DataMember(Name = "clicked")]
        public bool? Clicked { get; set; }

        [DataMember(Name = "profile_name")]
        public string ProfileName { get; set; }

        [DataMember(Name = "action_url")]
        public Uri ActionUrl { get; set; }

        [DataMember(Name = "destination")]
        public string Destination { get; set; }

        [DataMember(Name = "actions")]
        public string[] Actions { get; set; }

        [DataMember(Name = "latest_reel_media")]
        public ulong? LatestReelMedia { get; set; }

        [DataMember(Name = "comment_id")]
        public ulong? CommentId { get; set; }

        [DataMember(Name = "request_count")]
        public dynamic RequestCount { get; set; }

        [DataMember(Name = "inline_follow")]
        public InlineFollow InlineFollow { get; set; }

        [DataMember(Name = "comment_ids")]
        public ulong[] CommentIds { get; set; }

        [DataMember(Name = "second_profile_id")]
        public string SecondProfileId { get; set; }

        [DataMember(Name = "second_profile_image")]
        public dynamic SecondProfileImage { get; set; }

        [DataMember(Name = "profile_image_destination")]
        public dynamic ProfileImageDestination { get; set; }
    }
}