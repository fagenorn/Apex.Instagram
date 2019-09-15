using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Nametag
    {
        [DataMember(Name = "mode")]
        public int? Mode { get; set; }

        [DataMember(Name = "gradient")]
        public string Gradient { get; set; }

        [DataMember(Name = "emoji")]
        public string Emoji { get; set; }

        [DataMember(Name = "selfie_sticker")]
        public string SelfieSticker { get; set; }
    }
}