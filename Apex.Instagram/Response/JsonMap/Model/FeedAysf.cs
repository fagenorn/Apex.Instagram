using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class FeedAysf
    {
        [DataMember(Name = "landing_site_type")]
        public dynamic LandingSiteType { get; set; }

        [DataMember(Name = "uuid")]
        public string Uuid { get; set; }

        [DataMember(Name = "view_all_text")]
        public dynamic ViewAllText { get; set; }

        [DataMember(Name = "feed_position")]
        public dynamic FeedPosition { get; set; }

        [DataMember(Name = "landing_site_title")]
        public dynamic LandingSiteTitle { get; set; }

        [DataMember(Name = "is_dismissable")]
        public dynamic IsDismissable { get; set; }

        [DataMember(Name = "suggestions")]
        public Suggestion[] Suggestions { get; set; }

        [DataMember(Name = "should_refill")]
        public dynamic ShouldRefill { get; set; }

        [DataMember(Name = "display_new_unit")]
        public dynamic DisplayNewUnit { get; set; }

        [DataMember(Name = "fetch_user_details")]
        public dynamic FetchUserDetails { get; set; }

        [DataMember(Name = "title")]
        public dynamic Title { get; set; }

        [DataMember(Name = "activator")]
        public dynamic Activator { get; set; }
    }
}