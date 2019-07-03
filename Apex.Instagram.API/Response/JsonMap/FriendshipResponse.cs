using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class FriendshipResponse : Response
    {
        [DataMember(Name = "friendship_status")]
        public FriendshipStatus FriendshipStatus { get; set; }
    }
}