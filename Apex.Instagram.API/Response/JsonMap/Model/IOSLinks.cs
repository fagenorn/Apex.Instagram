﻿using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class IosLinks
    {
        [DataMember(Name = "linkType")]
        public int? LinkType { get; set; }

        [DataMember(Name = "canvasDocId")]
        public string CanvasDocId { get; set; }
    }
}