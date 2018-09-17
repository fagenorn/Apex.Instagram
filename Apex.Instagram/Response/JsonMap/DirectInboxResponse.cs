using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    internal class DirectInboxResponse : Response
    {
        [DataMember(Name = "pending_requests_total")]
        public dynamic PendingRequestsTotal { get; set; }

        [DataMember(Name = "seq_id")]
        public string SeqId { get; set; }

        [DataMember(Name = "pending_requests_users")]
        public User[] PendingRequestsUsers { get; set; }

        [DataMember(Name = "inbox")]
        public DirectInbox Inbox { get; set; }

        [DataMember(Name = "megaphone")]
        public Megaphone Megaphone { get; set; }

        [DataMember(Name = "snapshot_at_ms")]
        public string SnapshotAtMs { get; set; }
    }
}