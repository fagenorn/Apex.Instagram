using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class AndroidLinks
    {
        [DataMember(Name = "linkType")]
        public In LinkType { get; set; }

        [DataMember(Name = "webUri")]
        public string WebUri { get; set; }

        [DataMember(Name = "androidClass")]
        public string AndroidClass { get; set; }

        [DataMember(Name = "package")]
        public string Package { get; set; }

        [DataMember(Name = "deeplinkUri")]
        public string DeeplinkUri { get; set; }

        [DataMember(Name = "callToActionTitle")]
        public string CallToActionTitle { get; set; }

        [DataMember(Name = "redirectUri")]
        public string RedirectUri { get; set; }

        [DataMember(Name = "igUserId")]
        public string IgUserId { get; set; }

        [DataMember(Name = "leadGenFormId")]
        public string LeadGenFormId { get; set; }

        [DataMember(Name = "canvasDocId")]
        public string CanvasDocId { get; set; }
    }
}