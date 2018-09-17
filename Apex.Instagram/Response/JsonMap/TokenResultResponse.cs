using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class TokenResultResponse : Response
    {
        [DataMember(Name = "token")]
        public Token Token { get; set; }
    }
}