using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class StoryLocation
    {
        [DataMember(Name = "location")]
        public Location Location { get; set; }

        [DataMember(Name = "attribution")]
        public string Attribution { get; set; }

        [DataMember(Name = "is_hidden")]
        public int? IsHidden { get; set; }

        [DataMember(Name = "is_sticker")]
        public int? IsSticker { get; set; }
    }
}