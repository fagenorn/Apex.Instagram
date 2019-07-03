using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class ReelMention
    {
        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "is_hidden")]
        public int? IsHidden { get; set; }

        [DataMember(Name = "display_type")]
        public string DisplayType { get; set; }

        [DataMember(Name = "is_sticker")]
        public int? IsSticker { get; set; }
    }
}