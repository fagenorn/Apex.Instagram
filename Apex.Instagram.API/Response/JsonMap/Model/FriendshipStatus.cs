using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class FriendshipStatus
    {
        [DataMember(Name = "following")]
        public bool? Following { get; set; }

        [DataMember(Name = "followed_by")]
        public bool? FollowedBy { get; set; }

        [DataMember(Name = "incoming_request")]
        public bool? IncomingRequest { get; set; }

        [DataMember(Name = "outgoing_request")]
        public bool? OutgoingRequest { get; set; }

        [DataMember(Name = "is_private")]
        public bool? IsPrivate { get; set; }

        [DataMember(Name = "is_blocking_reel")]
        public bool? IsBlockingReel { get; set; }

        [DataMember(Name = "is_muting_reel")]
        public bool? IsMutingReel { get; set; }

        [DataMember(Name = "blocking")]
        public bool? Blocking { get; set; }

        [DataMember(Name = "muting")]
        public bool? Muting { get; set; }

        [DataMember(Name = "is_bestie")]
        public bool? IsBestie { get; set; }
    }
}