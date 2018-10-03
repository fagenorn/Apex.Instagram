using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonFormatter;

using Utf8Json;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class Nametag
    {
        [DataMember(Name = "mode")]
        public int? Mode { get; set; }

        [DataMember(Name = "gradient")]
        [JsonFormatter(typeof(DurableUlongFormatter))]
        public ulong? Gradient { get; set; }

        [DataMember(Name = "emoji")]
        public string Emoji { get; set; }

        [DataMember(Name = "selfie_sticker")]
        [JsonFormatter(typeof(DurableUlongFormatter))]
        public ulong? SelfieSticker { get; set; }
    }
}