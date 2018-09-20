using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Story
    {
        [DataMember(Name = "pk")]
        public ulong? Pk { get; set; }

        [DataMember(Name = "counts")]
        public Counts Counts { get; set; }

        [DataMember(Name = "args")]
        public Args Args { get; set; }

        [DataMember(Name = "type")]
        public int? Type { get; set; }

        [DataMember(Name = "story_type")]
        public int? StoryType { get; set; }
    }
}