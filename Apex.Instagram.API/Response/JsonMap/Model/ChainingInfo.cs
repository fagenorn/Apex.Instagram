﻿using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class ChainingInfo
    {
        [DataMember(Name = "sources")]
        public string Sources { get; set; }
    }
}