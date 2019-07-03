using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class PresencesResponse : Response
    {
        [DataMember(Name = "user_presence")]
        public dynamic UserPresence { get; set; }
    }
}