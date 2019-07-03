using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class TvChannel
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "title")]
        public string Title { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "items")]
        public Item[] Items { get; set; }

        [DataMember(Name = "more_available")]
        public bool? MoreAvailable { get; set; }

        [DataMember(Name = "max_id")]
        public string MaxId { get; set; }

        [DataMember(Name = "seen_state")]
        public dynamic SeenState { get; set; }

        [DataMember(Name = "user_dict")]
        public User UserDict { get; set; }
    }
}