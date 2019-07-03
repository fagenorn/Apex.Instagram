using System.Collections.Generic;
using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class FriendshipsShowManyResponse : Response
    {
        [DataMember(Name = "friendship_statuses")]
        public Dictionary<string, FriendshipStatus> FriendshipStatuses { get; set; }
    }
}