﻿using System;
using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonFormatter;

using Utf8Json;

namespace Apex.Instagram.Response.JsonMap.Model
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
        public string DeeplinkUri { get; set; }

        [DataMember(Name = "callToActionTitle")]
        public string CallToActionTitle { get; set; }

        [DataMember(Name = "redirectUri")]
        public string RedirectUri { get; set; }

        [DataMember(Name = "igUserId")]
        [JsonFormatter(typeof(DurableUlongFormatter))]
        public ulong? IgUserId { get; set; }

        [DataMember(Name = "leadGenFormId")]
        [JsonFormatter(typeof(DurableUlongFormatter))]
        public ulong? LeadGenFormId { get; set; }

        [DataMember(Name = "canvasDocId")]
        public string CanvasDocId { get; set; }
    }
}