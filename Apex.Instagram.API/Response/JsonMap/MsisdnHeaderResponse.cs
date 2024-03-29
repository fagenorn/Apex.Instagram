﻿using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class MsisdnHeaderResponse : Response
    {
        [DataMember(Name = "phone_number")]
        public string PhoneNumber { get; set; }

        [DataMember(Name = "url")]
        public Uri Url { get; set; }

        [DataMember(Name = "remaining_ttl_seconds")]
        public int? RemainingTtlSeconds { get; set; }

        [DataMember(Name = "ttl")]
        public int? Ttl { get; set; }
    }
}