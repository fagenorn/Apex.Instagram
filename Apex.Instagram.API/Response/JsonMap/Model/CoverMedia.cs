using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class CoverMedia
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "media_id")]
        public string MediaId { get; set; }

        [DataMember(Name = "media_type")]
        public int? MediaType { get; set; }

        [DataMember(Name = "image_versions2")]
        public ImageVersions2 ImageVersions2 { get; set; }

        [DataMember(Name = "original_width")]
        public int? OriginalWidth { get; set; }

        [DataMember(Name = "original_height")]
        public int? OriginalHeight { get; set; }

        [DataMember(Name = "cropped_image_version")]
        public ImageCandidate CroppedImageVersion { get; set; }

        [DataMember(Name = "crop_rect")]
        public int[] CropRect { get; set; }

        [DataMember(Name = "full_image_version")]
        public ImageCandidate FullImageVersion { get; set; }
    }
}