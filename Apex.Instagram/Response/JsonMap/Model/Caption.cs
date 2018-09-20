using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Caption
    {
        [DataMember(Name = "status")]
        public dynamic Status { get; set; }

        [DataMember(Name = "user_id")]
        public ulong? UserId { get; set; }

        [DataMember(Name = "created_at_utc")]
        public ulong? CreatedAtUtc { get; set; }

        [DataMember(Name = "created_at")]
        public ulong? CreatedAt { get; set; }

        [DataMember(Name = "bit_flags")]
        public int? BitFlags { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "content_type")]
        public dynamic ContentType { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "media_id")]
        public ulong? MediaId { get; set; }

        [DataMember(Name = "pk")]
        public ulong? Pk { get; set; }

        [DataMember(Name = "type")]
        public dynamic Type { get; set; }

        [DataMember(Name = "has_translation")]
        public bool? HasTranslation { get; set; }

        [DataMember(Name = "did_report_as_spam")]
        public bool? DidReportAsSpam { get; set; }
    }
}