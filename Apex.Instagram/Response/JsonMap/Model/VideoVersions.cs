﻿using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class VideoVersions
    {
        [DataMember(Name = "type")]
        public int? Type { get; set; }

        [DataMember(Name = "width")]
        public int? Width { get; set; }

        [DataMember(Name = "height")]
        public int? Height { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
}