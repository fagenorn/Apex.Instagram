﻿using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Action
    {
        [DataMember(Name = "title")]
        public Text Title { get; set; }

        [DataMember(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "limit")]
        public int? Limit { get; set; }

        [DataMember(Name = "dismiss_promotion")]
        public bool? DismissPromotion { get; set; }
    }
}