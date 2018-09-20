using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap
{
    public class BlockedMediaResponse : Response

    {
        [DataMember(Name = "media_ids")]
        public dynamic MediaIds { get; set; }
    }
}