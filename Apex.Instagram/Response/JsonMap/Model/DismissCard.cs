using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class DismissCard
    {
        [DataMember(Name = "card_id")]
        public dynamic CardId { get; set; }

        [DataMember(Name = "image_url")]
        public string ImageUrl { get; set; }

        [DataMember(Name = "title")]
        public dynamic Title { get; set; }

        [DataMember(Name = "message")]
        public dynamic Message { get; set; }

        [DataMember(Name = "button_text")]
        public dynamic ButtonText { get; set; }

        [DataMember(Name = "camera_target")]
        public dynamic CameraTarget { get; set; }

        [DataMember(Name = "face_filter_id")]
        public dynamic FaceFilterId { get; set; }
    }
}