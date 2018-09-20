﻿using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class ActionLog
    {
        [DataMember(Name = "bold")]
        public Bold[] Bold { get; set; }

        [DataMember(Name = "description")]
        public dynamic Description { get; set; }
    }
}