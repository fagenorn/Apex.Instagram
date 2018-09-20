using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class DirectReaction
    {
        [DataMember(Name = "reaction_type")]
        public string ReactionType { get; set; }

        [DataMember(Name = "timestamp")]
        public string Timestamp { get; set; }

        [DataMember(Name = "sender_id")]
        public string SenderId { get; set; }

        [DataMember(Name = "client_context")]
        public string ClientContext { get; set; }

        [DataMember(Name = "reaction_status")]
        public string ReactionStatus { get; set; }

        [DataMember(Name = "node_type")]
        public string NodeType { get; set; }

        [DataMember(Name = "item_id")]
        public string ItemId { get; set; }
    }
}