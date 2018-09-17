using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class TopLive
    {
        [DataMember(Name = "broadcast_owners")]
        public User[] BroadcastOwners { get; set; }

        [DataMember(Name = "ranked_position")]
        public dynamic RankedPosition { get; set; }
    }
}