using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class TraySuggestions
    {
        [DataMember(Name = "tray")]
        public StoryTray[] Tray { get; set; }

        [DataMember(Name = "tray_title")]
        public string TrayTitle { get; set; }

        [DataMember(Name = "banner_title")]
        public string BannerTitle { get; set; }

        [DataMember(Name = "banner_subtitle")]
        public string BannerSubtitle { get; set; }

        [DataMember(Name = "suggestion_type")]
        public string SuggestionType { get; set; }
    }
}