using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class BlockedMediaResponse : Response
    {
        [DataMember(Name = "media_ids")]
        public dynamic MediaIds { get; set; }
    }
}