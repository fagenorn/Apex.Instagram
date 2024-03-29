﻿using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class AndroidLinks
    {
        [DataMember(Name = "linkType")]
        public int? LinkType { get; set; }

        [DataMember(Name = "webUri")]
        public Uri WebUri { get; set; }

        [DataMember(Name = "androidClass")]
        public string AndroidClass { get; set; }

        [DataMember(Name = "package")]
        public string Package { get; set; }

        [DataMember(Name = "deeplinkUri")]
        public Uri DeeplinkUri { get; set; }

        [DataMember(Name = "callToActionTitle")]
        public string CallToActionTitle { get; set; }

        [DataMember(Name = "redirectUri")]
        public string RedirectUri { get; set; }

        [DataMember(Name = "igUserId")]
        public ulong? IgUserId { get; set; }

        [DataMember(Name = "appInstallObjectiveInvalidationBehavior")]
        public dynamic AppInstallObjectiveInvalidationBehavior { get; set; }

        [DataMember(Name = "tapAndHoldContext")]
        public string TapAndHoldContext { get; set; }

        [DataMember(Name = "leadGenFormId")]
        public ulong? LeadGenFormId { get; set; }

        [DataMember(Name = "canvasDocId")]
        public string CanvasDocId { get; set; }
    }
}