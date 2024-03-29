﻿using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Template
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "parameters")]
        public dynamic Parameters { get; set; }
    }
}