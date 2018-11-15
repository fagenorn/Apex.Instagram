﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Apex.Instagram.Tests.Maps
{
    public class HeadersJsonMap
    {
        [DataMember(Name = "headers")]
        public Dictionary<string, string> Headers { get; set; }
    }
}