using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class QuestionSticker
    {
        [DataMember(Name = "question_id")]
        public string QuestionId { get; set; }

        [DataMember(Name = "question")]
        public string Question { get; set; }

        [DataMember(Name = "text_color")]
        public string TextColor { get; set; }

        [DataMember(Name = "background_color")]
        public string BackgroundColor { get; set; }

        [DataMember(Name = "viewer_can_interact")]
        public bool? ViewerCanInteract { get; set; }

        [DataMember(Name = "profile_pic_url")]
        public string ProfilePicUrl { get; set; }

        [DataMember(Name = "question_type")]
        public string QuestionType { get; set; }
    }
}