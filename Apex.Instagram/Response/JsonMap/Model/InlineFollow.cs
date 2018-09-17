using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class InlineFollow
    {
        [DataMember(Name = "user_info")]
        public User UserInfo { get; set; }

        [DataMember(Name = "following")]
        public bool? Following { get; set; }

        [DataMember(Name = "outgoing_request")]
        public bool? OutgoingRequest { get; set; }
    }
}