﻿using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class StoryHashtag
    {
        [DataMember(Name = "hashtag")]
        public Hashtag InventorySource { get; set; }

        [DataMember(Name = "attribution")]
        public string Attribution { get; set; }

        [DataMember(Name = "custom_title")]
        public string CustomTitle { get; set; }

        [DataMember(Name = "is_hidden")]
        public int? IsHidden { get; set; }
    }
}