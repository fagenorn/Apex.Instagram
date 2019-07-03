using System.Runtime.Serialization;

namespace Apex.Instagram.API.Response.JsonMap.Model
{
    public class Responder
    {
        [DataMember(Name = "response")]
        public string Response { get; set; }

        [DataMember(Name = "has_shared_response")]
        public bool? HasSharedResponse { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }

        [DataMember(Name = "ts")]
        public int? Ts { get; set; }
    }
}