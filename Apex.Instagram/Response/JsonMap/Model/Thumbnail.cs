using System;
using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Thumbnail
    {
        [DataMember(Name = "video_length")]
        public double? VideoLength { get; set; }

        [DataMember(Name = "thumbnail_width")]
        public int? ThumbnailWidth { get; set; }

        [DataMember(Name = "thumbnail_height")]
        public int? ThumbnailHeight { get; set; }

        [DataMember(Name = "thumbnail_duration")]
        public double? ThumbnailDuration { get; set; }

        [DataMember(Name = "sprite_urls")]
        public Uri[] SpriteUrls { get; set; }

        [DataMember(Name = "thumbnails_per_row")]
        public int? ThumbnailsPerRow { get; set; }

        [DataMember(Name = "max_thumbnails_per_sprite")]
        public int? MaxThumbnailsPerSprite { get; set; }

        [DataMember(Name = "sprite_width")]
        public int? SpriteWidth { get; set; }

        [DataMember(Name = "sprite_height")]
        public int? SpriteHeight { get; set; }

        [DataMember(Name = "rendered_width")]
        public int? RenderedWidth { get; set; }
    }
}