using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class RecentSearchesResponse : Response
    {
        [DataMember(Name = "recent")]
        public Suggested[] Recent { get; set; }
    }
}