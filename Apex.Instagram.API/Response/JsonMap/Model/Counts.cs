using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Counts
    {
        [DataMember(Name = "relationships")]
        public dynamic Relationships { get; set; }

        [DataMember(Name = "requests")]
        public dynamic Requests { get; set; }

        [DataMember(Name = "photos_of_you")]
        public dynamic PhotosOfYou { get; set; }

        [DataMember(Name = "usertags")]
        public dynamic Usertags { get; set; }

        [DataMember(Name = "comments")]
        public dynamic Comments { get; set; }

        [DataMember(Name = "likes")]
        public dynamic Likes { get; set; }

        [DataMember(Name = "comment_likes")]
        public dynamic CommentLikes { get; set; }

        [DataMember(Name = "campaign_notification")]
        public dynamic CampaignNotification { get; set; }
    }
}