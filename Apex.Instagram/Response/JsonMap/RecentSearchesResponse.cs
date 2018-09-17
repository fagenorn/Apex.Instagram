using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class RecentSearchesResponse : Response
    {
        [DataMember(Name = "recent")]
        public Suggested[] Recent { get; set; }
    }
}