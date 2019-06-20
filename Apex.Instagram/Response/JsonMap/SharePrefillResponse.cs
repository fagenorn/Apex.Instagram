﻿using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class SharePrefillResponse : Response
    {
        [DataMember(Name = "ranking")]
        public Ranking[] Rankings { get; set; }

        [DataMember(Name = "entities")]
        public ServerDataInfo Entities { get; set; }

        [DataMember(Name = "failed_view_names")]
        public dynamic FailedViewNames { get; set; }
    }
}