﻿using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class StoriesNetego
    {
        [DataMember(Name = "tracking_token")]
        public string TrackingToken { get; set; }

        [DataMember(Name = "hide_unit_if_seen")]
        public string HideUnitIfSeen { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
}