using System.Runtime.Serialization;

using Apex.Instagram.API.Response.JsonMap.Model;

namespace Apex.Instagram.API.Response.JsonMap
{
    public class UserInfoResponse : Response
    {
        [DataMember(Name = "megaphone")]
        public dynamic Megaphone { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }
    }
}