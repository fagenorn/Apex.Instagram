using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class GenericMegaphone
    {
        [DataMember(Name = "type")]
        public dynamic Type { get; set; }

        [DataMember(Name = "pending_requests_total")]
        public dynamic PendingRequestsTotal { get; set; }

        [DataMember(Name = "title")]
        public dynamic Title { get; set; }

        [DataMember(Name = "message")]
        public dynamic Message { get; set; }

        [DataMember(Name = "dismissible")]
        public dynamic Dismissible { get; set; }

        [DataMember(Name = "icon")]
        public dynamic Icon { get; set; }

        [DataMember(Name = "buttons")]
        public Button[] Buttons { get; set; }

        [DataMember(Name = "megaphone_version")]
        public dynamic MegaphoneVersion { get; set; }

        [DataMember(Name = "button_layout")]
        public dynamic ButtonLayout { get; set; }

        [DataMember(Name = "action_info")]
        public dynamic ActionInfo { get; set; }

        [DataMember(Name = "button_location")]
        public dynamic ButtonLocation { get; set; }

        [DataMember(Name = "background_color")]
        public dynamic BackgroundColor { get; set; }

        [DataMember(Name = "title_color")]
        public dynamic TitleColor { get; set; }

        [DataMember(Name = "message_color")]
        public dynamic MessageColor { get; set; }

        [DataMember(Name = "uuid")]
        public string Uuid { get; set; }
    }
}