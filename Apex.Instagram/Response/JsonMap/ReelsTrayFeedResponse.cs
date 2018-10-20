using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class ReelsTrayFeedResponse : Response
    {
        [DataMember(Name = "story_ranking_token")]
        public string StoryRankingToken { get; set; }

        [DataMember(Name = "broadcasts")]
        public Broadcast[] Broadcasts { get; set; }

        [DataMember(Name = "tray")]
        public StoryTray[] Tray { get; set; }

        [DataMember(Name = "post_live")]
        public PostLive PostLive { get; set; }

        [DataMember(Name = "sticker_version")]
        public int? StickerVersion { get; set; }

        [DataMember(Name = "face_filter_nux_version")]
        public int? FaceFilterNuxVersion { get; set; }

        [DataMember(Name = "stories_viewer_gestures_nux_eligible")]
        public bool? StoriesViewerGesturesNuxEligible { get; set; }

        [DataMember(Name = "has_new_nux_story")]
        public bool? HasNewNuxStory { get; set; }

        [DataMember(Name = "suggestions")]
        public TraySuggestions[] Suggestions { get; set; }
    }
}