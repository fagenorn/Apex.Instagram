using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class StoryCountdown
    {
        [DataMember(Name = "x")]
        public double? X { get; set; }

        [DataMember(Name = "y")]
        public double? Y { get; set; }

        [DataMember(Name = "z")]
        public double? Z { get; set; }

        [DataMember(Name = "width")]
        public double? Width { get; set; }

        [DataMember(Name = "height")]
        public double? Height { get; set; }

        [DataMember(Name = "rotation")]
        public double? Rotation { get; set; }

        [DataMember(Name = "is_pinned")]
        public int? IsPinned { get; set; }

        [DataMember(Name = "is_hidden")]
        public int? IsHidden { get; set; }

        [DataMember(Name = "countdown_sticker")]
        public CountdownSticker CountdownSticker { get; set; }
    }
}