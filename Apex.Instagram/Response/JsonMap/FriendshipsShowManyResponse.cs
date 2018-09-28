using System.Collections.Generic;
using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class FriendshipsShowManyResponse : Response
    {
        [DataMember(Name = "friendship_statuses")]
        public Dictionary<string, FriendshipStatus> FriendshipStatuses { get; set; }
    }
}