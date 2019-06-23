using System.Runtime.Serialization;

using Apex.Instagram.Response.Serializer;

using Utf8Json;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Nametag
    {
        [DataMember(Name = "mode")]
        public int? Mode { get; set; }

        [DataMember(Name = "gradient")]
        [JsonFormatter(typeof(DurableStringFormatter))]
        public string Gradient { get; set; }

        [DataMember(Name = "emoji")]
        public string Emoji { get; set; }

        [DataMember(Name = "selfie_sticker")]
        [JsonFormatter(typeof(DurableStringFormatter))]
        public string SelfieSticker { get; set; }
    }
}