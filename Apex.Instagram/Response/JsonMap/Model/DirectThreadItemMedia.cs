using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class DirectThreadItemMedia
    {
        public const int Photo = 1;

        public const int Video = 2;

        [DataMember(Name = "media_type")]
        public int? MediaType { get; set; }

        [DataMember(Name = "image_versions2")]
        public ImageVersions2 ImageVersions2 { get; set; }

        [DataMember(Name = "video_versions")]
        public VideoVersions[] VideoVersions { get; set; }

        [DataMember(Name = "original_width")]
        public int? OriginalWidth { get; set; }

        [DataMember(Name = "original_height")]
        public int? OriginalHeight { get; set; }
    }
}