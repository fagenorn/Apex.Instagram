using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Headline
    {
        [DataMember(Name = "content_type")]
        public dynamic ContentType { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "user_id")]
        public string UserId { get; set; }

        [DataMember(Name = "pk")]
        public string Pk { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "type")]
        public dynamic Type { get; set; }

        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }

        [DataMember(Name = "created_at_utc")]
        public string CreatedAtUtc { get; set; }

        [DataMember(Name = "media_id")]
        public string MediaId { get; set; }

        [DataMember(Name = "bit_flags")]
        public int? BitFlags { get; set; }

        [DataMember(Name = "status")]
        public dynamic Status { get; set; }
    }
}