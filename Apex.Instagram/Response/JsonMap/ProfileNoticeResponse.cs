using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap
{
    public class ProfileNoticeResponse : Response
    {
        [DataMember(Name = "has_change_password_megaphone")]
        public bool? HasChangePasswordMegaphone { get; set; }
    }
}