using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class CountdownSticker
    {
        [DataMember(Name = "countdown_id")]
        public string CountdownId { get; set; }

        [DataMember(Name = "end_ts")]
        public string EndTs { get; set; }

        [DataMember(Name = "text")]
        public string Text { get; set; }

        [DataMember(Name = "text_color")]
        public string TextColor { get; set; }

        [DataMember(Name = "start_background_color")]
        public string StartBackgroundColor { get; set; }

        [DataMember(Name = "end_background_color")]
        public string EndBackgroundColor { get; set; }

        [DataMember(Name = "digit_color")]
        public string DigitColor { get; set; }

        [DataMember(Name = "digit_card_color")]
        public string DigitCardColor { get; set; }

        [DataMember(Name = "following_enabled")]
        public bool? FollowingEnabled { get; set; }

        [DataMember(Name = "is_owner")]
        public bool? IsOwner { get; set; }

        [DataMember(Name = "attribution")]
        public dynamic Attribution { get; set; }

        [DataMember(Name = "viewer_is_following")]
        public bool? ViewerIsFollowing { get; set; }
    }
}