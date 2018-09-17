using System.Runtime.Serialization;

using JetBrains.Annotations;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class MediaData
    {
        [DataMember(Name = "image_versions2")]
        public ImageVersions2 ImageVersions2 { get; set; }

        [DataMember(Name = "original_width")]
        public int? OriginalWidth { get; set; }

        [DataMember(Name = "original_height")]
        public int? OriginalHeight { get; set; }

        [DataMember(Name = "media_type")]
        public int? MediaType { get; set; }

        [CanBeNull]
        [DataMember(Name = "video_versions")]
        public VideoVersions[] VideoVersions { get; set; }
    }
}