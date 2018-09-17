using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class SuggestedUsers
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "view_all_text")]
        public dynamic ViewAllText { get; set; }

        [DataMember(Name = "title")]
        public dynamic Title { get; set; }

        [DataMember(Name = "auto_dvance")]
        public dynamic AutoDvance { get; set; }

        [DataMember(Name = "type")]
        public dynamic Type { get; set; }

        [DataMember(Name = "tracking_token")]
        public string TrackingToken { get; set; }

        [DataMember(Name = "landing_site_type")]
        public dynamic LandingSiteType { get; set; }

        [DataMember(Name = "landing_site_title")]
        public dynamic LandingSiteTitle { get; set; }

        [DataMember(Name = "upsell_fb_pos")]
        public dynamic UpsellFbPos { get; set; }

        [DataMember(Name = "suggestions")]
        public Suggestion[] Suggestions { get; set; }

        [DataMember(Name = "netego_type")]
        public dynamic NetegoType { get; set; }
    }
}