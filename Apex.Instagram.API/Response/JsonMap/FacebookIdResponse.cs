using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class FacebookIdResponse : Response
    {
        [DataMember(Name = "fbid")]
        public string Fbid { get; set; }
    }
}