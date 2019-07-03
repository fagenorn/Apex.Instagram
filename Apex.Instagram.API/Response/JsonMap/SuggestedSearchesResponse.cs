using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class SuggestedSearchesResponse : Response
    {
        [DataMember(Name = "suggested")]
        public Suggested[] Suggested { get; set; }

        [DataMember(Name = "rank_token")]
        public string RankToken { get; set; }
    }
}