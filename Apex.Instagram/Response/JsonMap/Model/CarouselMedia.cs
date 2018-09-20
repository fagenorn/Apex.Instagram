using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class CarouselMedia
    {
        public const int Photo = 1;

        public const int Video = 2;

        [DataMember(Name = "pk")]
        public ulong? Pk { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "carousel_parent_id")]
        public string CarouselParentId { get; set; }

        [DataMember(Name = "fb_user_tags")]
        public Usertag FbUserTags { get; set; }

        [DataMember(Name = "number_of_qualities")]
        public int? NumberOfQualities { get; set; }

        [DataMember(Name = "is_dash_eligible")]
        public int? IsDashEligible { get; set; }

        [DataMember(Name = "video_dash_manifest")]
        public string VideoDashManifest { get; set; }

        [DataMember(Name = "image_versions2")]
        public ImageVersions2 ImageVersions2 { get; set; }

        [DataMember(Name = "video_versions")]
        public VideoVersions[] VideoVersions { get; set; }

        [DataMember(Name = "has_audio")]
        public bool? HasAudio { get; set; }

        [DataMember(Name = "video_duration")]
        public double? VideoDuration { get; set; }

        [DataMember(Name = "video_subtitles_uri")]
        public string VideoSubtitlesUri { get; set; }

        [DataMember(Name = "original_height")]
        public int? OriginalHeight { get; set; }

        [DataMember(Name = "original_width")]
        public int? OriginalWidth { get; set; }

        [DataMember(Name = "media_type")]
        public int? MediaType { get; set; }

        [DataMember(Name = "usertags")]
        public Usertag Usertags { get; set; }

        [DataMember(Name = "preview")]
        public string Preview { get; set; }

        [DataMember(Name = "headline")]
        public Headline Headline { get; set; }

        [DataMember(Name = "link")]
        public string Link { get; set; }

        [DataMember(Name = "link_text")]
        public string LinkText { get; set; }

        [DataMember(Name = "link_hint_text")]
        public string LinkHintText { get; set; }

        [DataMember(Name = "android_links")]
        public AndroidLinks[] AndroidLinks { get; set; }

        [DataMember(Name = "ad_metadata")]
        public AdMetadata[] AdMetadata { get; set; }

        [DataMember(Name = "ad_action")]
        public string AdAction { get; set; }

        [DataMember(Name = "ad_link_type")]
        public int? AdLinkType { get; set; }

        [DataMember(Name = "force_overlay")]
        public bool? ForceOverlay { get; set; }

        [DataMember(Name = "hide_nux_text")]
        public bool? HideNuxText { get; set; }

        [DataMember(Name = "overlay_text")]
        public string OverlayText { get; set; }

        [DataMember(Name = "overlay_title")]
        public string OverlayTitle { get; set; }

        [DataMember(Name = "overlay_subtitle")]
        public string OverlaySubtitle { get; set; }

        [DataMember(Name = "dominant_color")]
        public string DominantColor { get; set; }
    }
}