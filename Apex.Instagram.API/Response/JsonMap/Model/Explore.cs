﻿using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Explore
    {
        [DataMember(Name = "explanation")]
        public dynamic Explanation { get; set; }

        [DataMember(Name = "actor_id")]
        public ulong? ActorId { get; set; }

        [DataMember(Name = "source_token")]
        public dynamic SourceToken { get; set; }
    }
}