using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Injected
    {
        [DataMember(Name = "label")]
        public string Label { get; set; }

        [DataMember(Name = "show_icon")]
        public bool? ShowIcon { get; set; }

        [DataMember(Name = "hide_label")]
        public string HideLabel { get; set; }

        [DataMember(Name = "invalidation")]
        public dynamic Invalidation { get; set; }

        [DataMember(Name = "is_demo")]
        public bool? IsDemo { get; set; }

        [DataMember(Name = "view_tags")]
        public dynamic ViewTags { get; set; }

        [DataMember(Name = "is_holdout")]
        public bool? IsHoldout { get; set; }

        [DataMember(Name = "tracking_token")]
        public string TrackingToken { get; set; }

        [DataMember(Name = "show_ad_choices")]
        public bool? ShowAdChoices { get; set; }

        [DataMember(Name = "ad_title")]
        public string AdTitle { get; set; }

        [DataMember(Name = "about_ad_params")]
        public string AboutAdParams { get; set; }

        [DataMember(Name = "direct_share")]
        public bool? DirectShare { get; set; }

        [DataMember(Name = "ad_id")]
        public ulong? AdId { get; set; }

        [DataMember(Name = "display_viewability_eligible")]
        public bool? DisplayViewabilityEligible { get; set; }

        [DataMember(Name = "fb_page_url")]
        public Uri FbPageUrl { get; set; }

        [DataMember(Name = "hide_reasons_v2")]
        public HideReason[] HideReasonsV2 { get; set; }

        [DataMember(Name = "hide_flow_type")]
        public int? HideFlowType { get; set; }

        [DataMember(Name = "cookies")]
        public string[] Cookies { get; set; }

        [DataMember(Name = "lead_gen_form_id")]
        public ulong? LeadGenFormId { get; set; }
    }
}