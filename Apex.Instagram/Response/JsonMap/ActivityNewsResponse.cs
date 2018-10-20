using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class ActivityNewsResponse : Response
    {
        [DataMember(Name = "new_stories")]
        public Story[] NewStories { get; set; }

        [DataMember(Name = "old_stories")]
        public Story[] OldStories { get; set; }

        [DataMember(Name = "continuation")]
        public dynamic Continuation { get; set; }

        [DataMember(Name = "friend_request_stories")]
        public Story[] FriendRequestStories { get; set; }

        [DataMember(Name = "counts")]
        public Counts Counts { get; set; }

        [DataMember(Name = "subscription")]
        public Subscription Subscription { get; set; }

        [DataMember(Name = "partition")]
        public dynamic Partition { get; set; }

        [DataMember(Name = "continuation_token")]
        public dynamic ContinuationToken { get; set; }

        [DataMember(Name = "ads_manager")]
        public dynamic AdsManager { get; set; }

        [DataMember(Name = "aymf")]
        public Aymf Aymf { get; set; }
    }
}