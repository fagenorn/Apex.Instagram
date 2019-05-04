using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class AnimatedMediaImageFixedHeigth
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "width")]
        public string Width { get; set; }

        [DataMember(Name = "heigth")]
        public string Heigth { get; set; }

        [DataMember(Name = "size")]
        public string Size { get; set; }

        [DataMember(Name = "mp4")]
        public string Mp4 { get; set; }

        [DataMember(Name = "mp4_size")]
        public string Mp4Size { get; set; }

        [DataMember(Name = "webp")]
        public string Webp { get; set; }

        [DataMember(Name = "webp_size")]
        public string WebpSize { get; set; }
    }
}