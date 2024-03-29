﻿using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Message
    {
        [DataMember(Name = "key")]
        public dynamic Key { get; set; }

        [DataMember(Name = "time")]
        public dynamic Time { get; set; }
    }
}