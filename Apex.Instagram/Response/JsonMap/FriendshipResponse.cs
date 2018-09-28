using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class FriendshipResponse : Response
    {
        [DataMember(Name = "friendship_status")]
        public FriendshipStatus FriendshipStatus { get; set; }
    }
}