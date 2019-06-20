using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class ServerDataInfo
    {
        [DataMember(Name = "cluster")]
        public string Cluster { get; set; }

        [DataMember(Name = "nonce")]
        public string Nonce { get; set; }

        [DataMember(Name = "conferenceName")]
        public string ConferenceName { get; set; }
    }
}