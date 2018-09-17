using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Nametag
    {
        [DataMember(Name = "mode")]
        public int? Mode { get; set; }

        [DataMember(Name = "gradient")]
        public int? Gradient { get; set; }

        [DataMember(Name = "emoji")]
        public string Emoji { get; set; }

        [DataMember(Name = "selfie_sticker")]
        public int? SelfieSticker { get; set; }
    }
}