using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class BootstrapUsersResponse : Response
    {
        [DataMember(Name = "surfaces")]
        public Surface[] Surfaces { get; set; }

        [DataMember(Name = "users")]
        public User[] Users { get; set; }
    }
}