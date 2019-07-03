using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class ActionBadge
    {
        [DataMember(Name = "action_type")]
        public dynamic ActionType { get; set; }

        [DataMember(Name = "action_count")]
        public dynamic ActionCount { get; set; }

        [DataMember(Name = "action_timestamp")]
        public dynamic ActionTimestamp { get; set; }
    }
}