using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Link
    {
        [DataMember(Name = "start")]
        public int? Start { get; set; }

        [DataMember(Name = "end")]
        public int? End { get; set; }

        [DataMember(Name = "id")]
        public ulong? Id { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "link_context")]
        public LinkContext LinkContext { get; set; }
    }
}