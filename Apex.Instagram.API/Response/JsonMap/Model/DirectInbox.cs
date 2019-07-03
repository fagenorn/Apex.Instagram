using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class DirectInbox
    {
        [DataMember(Name = "has_older")]
        public bool? HasOlder { get; set; }

        [DataMember(Name = "unseen_count")]
        public dynamic UnseenCount { get; set; }

        [DataMember(Name = "unseen_count_ts")]
        public dynamic UnseenCountTs { get; set; }

        [DataMember(Name = "blended_inbox_enabled")]
        public bool? BlendedInboxEnabled { get; set; }

        [DataMember(Name = "oldest_cursor")]
        public dynamic OldestCursor { get; set; }

        [DataMember(Name = "threads")]
        public DirectThread[] Threads { get; set; }
    }
}