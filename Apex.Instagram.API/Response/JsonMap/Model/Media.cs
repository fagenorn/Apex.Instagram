using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Media
    {
        [DataMember(Name = "image")]
        public string Image { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "expiring_at")]
        public dynamic ExpiringAt { get; set; }

        [DataMember(Name = "comment_threading_enabled")]
        public bool? CommentThreadingEnabled { get; set; }
    }
}