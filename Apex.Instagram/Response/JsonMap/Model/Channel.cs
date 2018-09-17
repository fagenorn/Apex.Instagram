using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Channel
    {
        [DataMember(Name = "channel_id")]
        public string ChannelId { get; set; }

        [DataMember(Name = "channel_type")]
        public dynamic ChannelType { get; set; }

        [DataMember(Name = "title")]
        public dynamic Title { get; set; }

        [DataMember(Name = "header")]
        public dynamic Header { get; set; }

        [DataMember(Name = "media_count")]
        public int? MediaCount { get; set; }

        [DataMember(Name = "media")]
        public Item Media { get; set; }

        [DataMember(Name = "context")]
        public dynamic Context { get; set; }
    }
}