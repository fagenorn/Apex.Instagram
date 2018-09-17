using System.Runtime.Serialization;

namespace Apex.Instagram.Response.JsonMap.Model
{
    public class DirectRankedRecipient
    {
        [DataMember(Name = "thread")]
        public DirectThread Thread { get; set; }

        [DataMember(Name = "user")]
        public User User { get; set; }
    }
}