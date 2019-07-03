using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Creative
    {
        [DataMember(Name = "title")]
        public Text Title { get; set; }

        [DataMember(Name = "content")]
        public Text Content { get; set; }

        [DataMember(Name = "footer")]
        public Text Footer { get; set; }

        [DataMember(Name = "social_context")]
        public Text SocialContext { get; set; }

        [DataMember(Name = "primary_action")]
        public Action PrimaryAction { get; set; }

        [DataMember(Name = "secondary_action")]
        public Action SecondaryAction { get; set; }

        [DataMember(Name = "dismiss_action")]
        public dynamic DismissAction { get; set; }

        [DataMember(Name = "image")]
        public Image Image { get; set; }
    }
}