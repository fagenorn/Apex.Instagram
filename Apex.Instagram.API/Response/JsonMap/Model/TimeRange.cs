﻿using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class TimeRange
    {
        [DataMember(Name = "start")]
        public ulong? Start { get; set; }

        [DataMember(Name = "end")]
        public ulong? End { get; set; }
    }
}