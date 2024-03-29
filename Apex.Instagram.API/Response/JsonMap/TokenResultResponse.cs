﻿using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class TokenResultResponse : Response
    {
        [DataMember(Name = "token")]
        public Token Token { get; set; }
    }
}