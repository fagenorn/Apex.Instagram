using System.Runtime.Serialization;

using Apex.Instagram.Response.JsonMap.Model;

namespace Apex.Instagram.Response.JsonMap
{
    public class BootstrapUsersResponse : Response
    {
        [DataMember(Name = "surfaces")]
        public Surface[] Surfaces { get; set; }

        [DataMember(Name = "users")]
        public User[] Users { get; set; }
    }
}