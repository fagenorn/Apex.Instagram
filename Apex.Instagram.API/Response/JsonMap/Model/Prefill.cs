﻿using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Prefill
    {
        [DataMember(Name = "usage")]
        public string Usage { get; set; }

        [DataMember(Name = "candidates")]
        public dynamic Candidates { get; set; }
    }
}